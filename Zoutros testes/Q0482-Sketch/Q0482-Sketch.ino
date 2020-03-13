/*
     Q0482-Sketch.ino
     AUTOR:   eu
     LINK:    https://www.youtube.com/brincandocomideias ; https://cursodearduino.net/
     COMPRE:  https://www.arducore.com.br/
     SKETCH:  Data Logger com Envio ao Excel via Teclado (Para Arduino Leonardo ou Compatível)
     DATA:    20/07/2019

   ATUALIZACAO: em ../../.... por .... o que .......

*/
//SENSOR DE TEMPERATURA E HUMIDADE RELATIVA
#include <dht.h>

//RELOGIO RTC
#include <Wire.h>
#include "RTClib.h"

//OBJETO PARA EMULADOR DE TECLADO (ARDUINO LEONARDO OU COMPATIVEL)
#include <SoftwareSerial.h>
SoftwareSerial oSerial(2, 3); // RX, TX

//CRIACAO DOS OBJETOS
dht DHT;
RTC_DS3231 rtc;

//PINAGEM DO CIRCUITO
#define pinPause 12
#define DHT22_PIN 4
//o RTC deve ser ligado nas portas I2C (SDA e SCK). Verifique o diagrama de pinos do seu Arduino.

//VARIAVEIS GLOBAIS
char daysOfTheWeek[7][12] = {"Domingo", "Segunda", "Terca", "Quarta", "Quinta", "Sexta", "Sabado"};

//DECLARACAO DAS FUNCOES
void estadoPause();
void erroCritico();
void teclaEnter();
void teclaTAB();

//EXECUTADO QUANDO O ARDUINO FOR LIGADO
void setup() {
  //INICIO DOS PINOS
  pinMode(pinPause, INPUT_PULLUP);

  //INICIA RTC
  if (! rtc.begin()) {
    erroCritico();
  }

  //A LINHA DE BAIXO É USADA APENAS PARA AJUSTAR O HORARIO DO RTC APARTIR DA HORA DO COMPUTADOR
  //rtc.adjust(DateTime(F(__DATE__), F(__TIME__)));

  //INICIO DO EMULADOR DE TECLADO
  oSerial.begin();
}

//EXECUTADO REPETIDAMENTE APÓS A EXECUCAO DO SETUP
void loop() {

  //PARA FACILITAR FUTURAS CARGAS
  if (!digitalRead(pinPause)) {
    estadoPause();
    return;
  }

  DateTime now = rtc.now();

  oSerial.print( daysOfTheWeek[now.dayOfTheWeek()] );
  teclaTAB();

  oSerial.print(now.hour());
  oSerial.print(':');
  oSerial.print(now.minute());
  oSerial.print(':');
  oSerial.print(now.second());
  teclaTAB();

  oSerial.print(now.day());
  oSerial.print('/');
  oSerial.print(now.month());
  oSerial.print('/');
  oSerial.print(now.year());
  teclaTAB();

  int chk = DHT.read22(DHT22_PIN);
  if (chk == DHTLIB_OK) {

    String umidade_string;
    umidade_string = String(DHT.humidity, 2);
    umidade_string.replace(".", ",");

    oSerial.print(umidade_string);
    teclaTAB();

    String temperatura_string;
    temperatura_string = String(DHT.temperature, 2);
    temperatura_string.replace(".", ",");

    oSerial.print(temperatura_string);
    teclaTAB();
  }

  teclaEnter();

  delay(2000);
}

//FUNCAO PARA AGUARDAR ANTES DE INICIAR O SETUP PARA NAO PROJUDICAR FUTURAS CARGAS
void estadoPause() {
  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)
  delay(100);                       // wait for a second
  digitalWrite(LED_BUILTIN, LOW);    // turn the LED off by making the voltage LOW
  delay(900);                       // wait for a second
}

//FUNCAO PARA AVISO DE ERRO CRITICO ATRAVES DO LED INTERNO DO ARDUINO
void erroCritico() {
  pinMode(LED_BUILTIN, OUTPUT);
  while (true) {
    digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)
    delay(200);                       // wait for a second
    digitalWrite(LED_BUILTIN, LOW);    // turn the LED off by making the voltage LOW
    delay(200);                       // wait for a second
  }
}

//FUNCAO PARA ENVIAR O APERTO DE UM ENTER
void teclaEnter() {
  oSerial.println();
}

//FUNCAO PARA ENVIAR O APERTO DE UM TAB
void teclaTAB() {
    oSerial.print("\t");
}
