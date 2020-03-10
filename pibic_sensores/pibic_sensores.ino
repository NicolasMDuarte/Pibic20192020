//-------
//RELÓGIO
//-------
#include <Wire.h> 
#include "RTClib.h" 
RTC_DS1307 relogio; //OBJETO DO TIPO RTC_DS1307

//--------------------------
// SENSOR DE UMIDADE DO SOLO
//--------------------------
#define pino_sensor_umidade A0
int umidade_solo;

//---------------------------
// SENSOR DE LUMINANCIA GY-30
//---------------------------
#include <BH1750.h>
#include <Wire.h>

//--------------------------------------
//SENSOR  TEMPERATURA  E  UMIDADE  DHT11
//--------------------------------------
#include "dht.h"
#define DHTPIN A1 // pino que estamos conectado
#define DHTTYPE DHT11 // DHT 11
DHT DHTHT(DHTPIN, dhtHTTYPE);

//--------------------------------------
// SENSOR DE TEMPERATURA E PRESSÃO DHT11
//--------------------------------------
#include <Wire.h>
#include <Adafruit_BMP085.h>
Adafruit_BMP085 bmp180;
int mostrador = 0; // pra q meu pai?

//---------------------
// SENSOR DE GAS MQ-135
//---------------------
#define MQ_analog A2
#define MQ_dig 7 
int valor_mq135;
int valor_detec;

void setup() {
  
  Serial.begin(9600); //INICIALIZA A SERIAL
  
  if (!relogio.begin())  // SE O RTC NÃO FOR INICIALIZADO
  { 
    Serial.println("DS1307 não encontrado"); 
    while(1); //SEMPRE ENTRE NO LOOP  ?? QUE
  }
  if (!relogio.isrunning()) //SE RTC NÃO ESTIVER SENDO EXECUTADO
  { 
    Serial.println("DS1307 rodando!"); 
    relogio.adjust(DateTime(F(_DATE), F(TIME_))); //CAPTURA A DATA E HORA EM QUE O SKETCH É COMPILADO
  }
  //delay(100); // precisa deixar esse delay?
  
  pinMode(pino_sensor_umidade, INPUT); 

  if (!lightSensor.begin()) 
  {
    Serial.println("Sensor bmp180 nao encontrado !!");
    while (1) {}
  }
  
  if (!dhtHT.begin()) 
  {
    Serial.println("Sensor bmp180 nao encontrado !!");
    while (1) {}
  }

  if (!bmp180.begin()) 
  {
    Serial.println("Sensor bmp180 nao encontrado !!");
    while (1) {}
  }

  pinMode(MQ_analog, INPUT);
  pinMode(MQ_dig, INPUT);
}

void loop() {
//-------
//RELÓGIO
//-------
DateTime now = relogio.now(); 
Serial.print(now.day(), DEC);
Serial.print('/'); 
Serial.print(now.month(), DEC); 
Serial.print('/'); 
Serial.print(now.year(), DEC);
Serial.print("/t"); 

Serial.print(now.hour(), DEC); 
Serial.print(':'); 
Serial.print(now.minute(), DEC); 
Serial.print(':'); 
Serial.print(now.second(), DEC); 
// imprimir milessegundos
Serial.print("/t"); 

//--------------------------
// SENSOR DE UMIDADE DO SOLO
//--------------------------
umidade_solo = analogRead(pino_sensor_umidade);//Le o valor do pino A0 do sensor

Serial.print(umidade_solo);
Serial.print("/t");

//---------------------------
// SENSOR DE LUMINANCIA GY-30
//---------------------------
uint16_t lux = lightSensor.readLightLevel();
luz = String(lux);

Serial.print(luz);
Serial.print("/t");

//--------------------------------------
//SENSOR  TEMPERATURA  E  UMIDADE  DHT11
//--------------------------------------
// A leitura da temperatura e umidade pode levar 250ms!
// O atraso do sensor pode chegar a 2 segundos.
float h = dht.readHumidity();
float t = dht.readTemperature();
// testa se retorno é valido, caso contrário algo está errado.
if (isnan(t) || isnan(h)) 
  Serial.println("Falha ao ler DHT (Temp e Umid)");
else
{
  Serial.print(h);
  Serial.print("/t");
  Serial.print(t);
  Serial.println("/t");
}

//--------------------------------------
// SENSOR DE TEMPERATURA E PRESSÃO DHT11
//--------------------------------------
Serial.print("Temperatura : ");
if ( bmp180.readTemperature() < 10)
 Serial.print(bmp180.readTemperature());
else
 Serial.print(bmp180.readTemperature(),1);   
Serial.print("/t");
   
if (mostrador == 0)
{
 Serial.print(bmp180.readAltitude());
 Serial.println("/t");
}
if (mostrador == 1)
{
 Serial.print(bmp180.readPressure());  
 Serial.println("/t");
}
//delay(3000); PRECISA?
mostrador = !mostrador;

//---------------------
// SENSOR DE GAS MQ-135
//---------------------
valor_mq135 = analogRead(MQ_analog); 
valor_detec = digitalRead(MQ_dig);

if(valor_dig == 0)
 Serial.print("DETECTADO");
else 
 Serial.print("AUSENTE");
Serial.print("/t");
 
Serial.print(valor_analog);

delay(100);
}
