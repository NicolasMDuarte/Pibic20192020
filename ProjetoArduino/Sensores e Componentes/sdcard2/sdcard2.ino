 

 
 
#include <SdFat.h>  // baseado no codigo do felipeflop
#include <Adafruit_Sensor.h> //INCLUSÃO DE BIBLIOTECA
#include <Adafruit_BMP280.h> //INCLUSÃO DE BIBLIOTECA
 
SdFat sdCard;
SdFile meuArquivo;
Adafruit_BMP280 bmp; //OBJETO DO TIPO Adafruit_BMP280 (I2C) 
// Pino ligado ao CS do modulo
const int chipSelect = 4;
 
void setup()
{
  Serial.begin(9600);
  // Inicializa o modulo SD
  if(!sdCard.begin(chipSelect,SPI_HALF_SPEED)){
     sdCard.initErrorHalt();
     Serial.println("nao inicilizou");
  }
  // Abre o arquivo LER_POT.TXT
  //if (!meuArquivo.open("LER_POT.TXT", O_RDWR | O_CREAT | O_AT_END))
  if (!meuArquivo.open("dados2.txt", O_WRONLY | O_CREAT | O_AT_END))
  {
    sdCard.errorHalt("Erro na abertura do arquivo LER_POT.TXT!");
  }else{
    Serial.println("inicilizou");
  }

  if(!bmp.begin(0x76)){ //SE O SENSOR NÃO FOR INICIALIZADO NO ENDEREÇO I2C 0x76, FAZ
    Serial.println(F("Sensor BMP280 não foi identificado! Verifique as conexões.")); //IMPRIME O TEXTO NO MONITOR SERIAL
    while(1); //SEMPRE ENTRE NO LOOP
  }

}

// todo
// colocar 3 leds para indicar se tudo foi inicializado e o segundo para indicar se o sensor de po esta ativo;
// led 3 para indicar se pode tirar o cartão;
int valor = 15;
void loop()
{
    // Leitura da bmp280
    meuArquivo.print(F("Temperatura: ")); //IMPRIME O TEXTO NO MONITOR SERIAL
    meuArquivo.print(bmp.readTemperature()); //IMPRIME NO MONITOR SERIAL A TEMPERATURA
    meuArquivo.println(" *C (Grau Celsius)"); //IMPRIME O TEXTO NO MONITOR SERIAL
    
    meuArquivo.print(F("Pressão: ")); //IMPRIME O TEXTO NO MONITOR SERIAL
    meuArquivo.print(bmp.readPressure()); //IMPRIME NO MONITOR SERIAL A PRESSÃO
    meuArquivo.println(" Pa (Pascal)"); //IMPRIME O TEXTO NO MONITOR SERIAL
 
    meuArquivo.print(F("Altitude aprox.: ")); //IMPRIME O TEXTO NO MONITOR SERIAL
    meuArquivo.print(bmp.readAltitude(1013.25),0); //IMPRIME NO MONITOR SERIAL A ALTITUDE APROXIMADA
    meuArquivo.println(" m (Metros)"); //IMPRIME O TEXTO NO MONITOR SERIAL

    if (valor < 5)
    {
      // Interrompe o processo e fecha o arquivo
      Serial.println("Processo de gravacao interrompido. Retire o SD!");
      meuArquivo.close();
      while (1) {}
    }
    delay(1000);
    valor--;
}
