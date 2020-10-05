/* PROJETO: PIBIC-EM 19/20 - PROJETO DRONE
 * DATA: 27/09/2020
 * AUTORES: 
 * Giovanna Pavani Martelli
 * Nicolas Duarte Maionese (vegana)
 * Sergio Luiz Moral Marques
 * 
 * Baseado em:
 * - Programa original de Krodal e na biblioteca virtuabotixRTC
 * - Códigos do FelipeFlop
 *   \ https://www.filipeflop.com/blog/relogio-rtc-ds1307-arduino/
 *   \ https://www.filipeflop.com/blog/cartao-sd-com-arduino/
 *   \ https://www.filipeflop.com/blog/monitorando-temperatura-e-umidade-com-o-sensor-dht11/
 * - Códigos da Arduino&Cia
 *   \ https://www.arduinoecia.com.br/bmp280-pressao-temperatura-altitude
 *   \ https://www.arduinoecia.com.br/BH1750-sensor-de-luminosidade-lux/
 *   \ https://www.arduinoecia.com.br/arduino-modulo-rtc-ds1302/
 * - programa de Christopher Nafis http://www.seeedstudio.com/depot/grove-dust-sensor-p-1050.html
 * 
 */

// INCLUSÃO DE BIBLIOTECAS
#include <SdFat.h>  
#include <Adafruit_Sensor.h> 
#include <Adafruit_BMP280.h>
#include <Wire.h>
#include <BH1750.h>
#include "DHT.h"
#include <virtuabotixRTC.h>           
#include <EEPROM.h> //limpador de memória

// DHT11
#define DHTPIN 2 
#define DHTTYPE DHT11 
DHT dht(DHTPIN, DHTTYPE);

// MÓDULO MICRO-SD
SdFat sdCard;
SdFile meuArquivo;
const int chipSelect = 4; // Pino ligado ao CS do modulo
//botão e leds para a gravação no cartão micro-sd
#define sd_open  10
#define sd_close 9
#define btn      3 

//BMP280
Adafruit_BMP280 bmp; //(I2C) 

//GY-30
BH1750 lightSensor;
String luz;

//RELÓGIO RTC3231
virtuabotixRTC myRTC(5, 6, 7); //myRTC(clock, data, rst) - Determina os pinos ligados ao modulo

//SENSOR DE PARTÍCULAS
int sensorValue;
int pin = 8;
unsigned long duration;
unsigned long starttime;
unsigned long sampletime_ms = 30000;//sampe 30s ;
unsigned long lowpulseoccupancy = 0;
float ratio = 0;
float concentration = 0;

// enumeração dos status do leitor sd p/ botao
enum fileStatus { 
  f_unKnow,
  f_open,
  f_working,
  f_close, 
  f_write,
};

//variáveis para manipulação do arquivo texto e sua gravação no micro-sd
const byte interruptPin = 3;
volatile fileStatus fs = f_write;
bool inicia = false;
int addr = 0;
int addr1 = 1;
int value;
char nome[20];

void setup()
{
  pinMode(sd_open, OUTPUT);
  pinMode(sd_close, OUTPUT);
  digitalWrite(sd_open, HIGH);
  digitalWrite(sd_close,HIGH);
  Serial.begin(9600);
  Serial.println("------------------------------------------");
  Serial.println("      PIBIC-EM 19/20 - PROJETO DRONE");
  Serial.println("------------------------------------------");
  Serial.println("Drone inicializando...");
  
  value = EEPROM.read(addr);
  if (value != 2){
    EEPROM.write(addr, 2);  
    value = 0;
    EEPROM.write(addr1, value+1);
  }
  else{
    value = EEPROM.read(addr1);
    EEPROM.write(addr1, value+1);
  }
  
  // Inicializa o modulo SD
  if(!sdCard.begin(chipSelect,SPI_HALF_SPEED)){
     sdCard.initErrorHalt();
     Serial.println("nao inicilizou");
     digitalWrite(sd_open, HIGH);
     digitalWrite(sd_close,HIGH);  
  }
  
  // Abre o arquivo DadosDrone.TXT
  sprintf(nome,"arquivo%d.txt",value);
  if (!meuArquivo.open(nome, O_WRITE | O_CREAT | O_AT_END)) // erro ao abrir arquivo
  {
    sprintf(nome,"Erro -> arquivo_%d.txt\n",value);
    sdCard.errorHalt(nome);
    digitalWrite(sd_open, HIGH);
    digitalWrite(sd_close,HIGH);  
  }
  else{ // sd ok
    for (int i = 0; i < 5; i++) // leds piscam sinalizando sucesso 
    {
      digitalWrite(sd_open, HIGH);
      digitalWrite(sd_close,HIGH);
      delay(100);
      digitalWrite(sd_open, LOW);
      digitalWrite(sd_close,LOW);
      delay(100);
    }
    digitalWrite(sd_open, HIGH);
    digitalWrite(sd_close,LOW);
    Serial.println(fs);
    
    // escreve aqui o cabecalho do arquivo csv
    meuArquivo.println("Data-Horário\tTemperatura (ºC)\tPressão (Pa)\tAltitude (m)\tLuz (lx)\tCO\tNH3\tNO2\tAir\tUmidade\tPartículas (ppm)");
  }

  // INICIALIZAÇÃO DOS SENSORES
  if(!bmp.begin(0x76)){ 
    Serial.println(F("Erro -> Sensor BMP280"));
    while(1); // entra em loop e n continua execução
  }
  lightSensor.begin();
  dht.begin();

  //Informacoes iniciais de data e hora
  //Apos settar as informacoes, comentar a linha abaixo
  //(segundos, minutos, hora, dia da semana, dia do mes, mes, ano)
  myRTC.setDS1302Time(00, 00, 12, 2, 27, 9, 2020);

  pinMode(8,INPUT);
  starttime = millis(); //pega horario atual

  // INICIALIZAÇÃO DOS LEDS, BOTAO E DA SERIAL
  pinMode(sd_open, OUTPUT);
  pinMode(sd_close, OUTPUT);
  pinMode(btn, INPUT_PULLUP);
  attachInterrupt(digitalPinToInterrupt(interruptPin), changeState, CHANGE);
  Serial.println(fs);
}

/* colocar 3 leds 
1 - para indicar se tudo foi inicializadO
2 - para indicar se o sensor de po esta ativo
3-  para indicar se pode tirar o cartão;Erro ->
*/
void loop()
{
  if (fs ==f_working)
  {   
    String separador = "\t";

    // Leitura do RTC
    myRTC.updateTime(); 

    // imprime "dd/mm/yyyy - hh:mm:ss"
    meuArquivo.print(myRTC.dayofmonth); //imprime o dia
    meuArquivo.print("/");
    meuArquivo.print(myRTC.month);      //imprime o mes
    meuArquivo.print("/");
    meuArquivo.print(myRTC.year);       //imprime o ano
    meuArquivo.print(" - ");
    
    if (myRTC.hours < 10)//Adiciona um 0 à esquerda caso o valor da hora caso seja <10
    {
      meuArquivo.print("0");
    }
    meuArquivo.print(myRTC.hours);
    meuArquivo.print(":");
    
    if (myRTC.minutes < 10) //Adiciona um 0 à esquerda caso o valor dos minutos seja <10
    {
      meuArquivo.print("0");
    }
    meuArquivo.print(myRTC.minutes);
    meuArquivo.print(":");
    
    if (myRTC.seconds < 10)//Adiciona um 0 caso o valor dos segundos seja <10
    { 
      meuArquivo.print("0");
    }
    meuArquivo.print(myRTC.seconds);
    meuArquivo.print(separador); // imprime separador p/ separar cada tipo de dado
    
    // Leitura do BMP280
    meuArquivo.print(bmp.readTemperature()); // imprime temperatura em ºC
    meuArquivo.print(separador);   
    meuArquivo.print(bmp.readPressure()); // imprime pressão em Pa
    meuArquivo.print(separador); 
    meuArquivo.print(bmp.readAltitude(1013.25),0); // imprime altitude aproximada em m
    meuArquivo.print(separador);
    
    // Leitura do GY-30
    uint16_t lux = lightSensor.readLightLevel();
    meuArquivo.print(String(lux)); // imprime luminosidade em lux
    meuArquivo.print(separador); 
    
    // Leitura do MICS 6814
    int co = analogRead(A1); // o sensor está ligado, respectivamente, no A1, A2 e A3
    int nh3 = analogRead(A2);
    int no2 = analogRead(A3);
    meuArquivo.print(co);  // imprime concentração de CO  em ppm
    meuArquivo.print(separador); 
    meuArquivo.print(nh3); // imprime concentração de NH3 em ppm
    meuArquivo.print(separador); 
    meuArquivo.print(no2); // imprime concentração de NO2 em ppm
    meuArquivo.print(separador); 
    
    // Leitura do MQ135
    sensorValue = analogRead(A0); // o sensor está ligado no analog input do pin 0
    meuArquivo.print(sensorValue, DEC); // imprime a conceitração de partículas em ppm
    meuArquivo.print(separador); 
    
    // Leitura do DHT11
    float u = dht.readHumidity();
    if (isnan(u)) // testa se retorno é valido, caso contrário algo está errado
      meuArquivo.println(" -1,");
    else
    {
      meuArquivo.print(u); // imprime a umidade em %
      meuArquivo.print(separador); 
    }

    // Leitura do sensor de particulas
    duration = pulseIn(pin, LOW);
    lowpulseoccupancy = lowpulseoccupancy+duration;
    if ((millis()-starttime) > sampletime_ms)//se já se passaram 30s desde a última coleta
    {
        ratio = lowpulseoccupancy/(sampletime_ms*10.0); // Integer porcentagem 0=>100
        concentration = 1.1*pow(ratio,3)-3.8*pow(ratio,2)+520*ratio+0.62; // cálculo específico do datasheet
        lowpulseoccupancy = 0;
        starttime = millis();
        meuArquivo.print(concentration); // imprime a concentracao de particulas em ppm
        meuArquivo.print(separador);
    }else // se ainda não deu o tempo de espera
      meuArquivo.println("-1"); 

    // fim da leitura dos sensores
    delay(1000);
    
    Serial.println(fs);
    if (fs == f_close)
    {
      Serial.println(fs);
      Serial.println(". Retire o SD!...");
      meuArquivo.close();
      delay(100);
      fs = f_unKnow;
      digitalWrite(sd_open, LOW);
      digitalWrite(sd_close,HIGH);
    }        

  }
   
  if (!inicia){
    fs =f_working;
    inicia = true;
  }
}

// troca status do sd
void changeState(){  
    if (fs == f_working)
      fs = f_close;
}
