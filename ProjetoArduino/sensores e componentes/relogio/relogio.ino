//Programa: Data e hora com modulo RTC DS1302
//Alteracoes e adaptacoes: Arduino e Cia
////Baseado no programa original de Krodal e na biblioteca virtuabotixRTC
//Carrega a biblioteca virtuabotixRTC
#include <virtuabotixRTC.h>           
//Determina os pinos ligados ao modulo
//myRTC(clock, data, rst)
virtuabotixRTC rtc(3, 5, 7);
void setup()  
{        
  Serial.begin(9600);  
  //Informacoes iniciais de data e hora  
  //Apos setar as informacoes, comente a linha abaixo  
  //(segundos, minutos, hora, dia da semana, dia do mes, mes, ano)  
  rtc.setDS1302Time(00, 58, 23, 2, 17, 11, 2014);
  }
  void loop()  
  {  //Le as informacoes do CI  
    rtc.updateTime();   
    //Imprime as informacoes no serial monitor  Serial.print("Data : ");  //Chama a rotina que imprime o dia da semana  imprime_dia_da_semana(myRTC.dayofweek);  Serial.print(", ");  Serial.print(myRTC.dayofmonth);  Serial.print("/");  Serial.print(myRTC.month);  Serial.print("/");  Serial.print(myRTC.year);  Serial.print("  ");  Serial.print("Hora : ");  //Adiciona um 0 caso o valor da hora seja <10  if (myRTC.hours < 10)  {    Serial.print("0");  }  Serial.print(myRTC.hours);  Serial.print(":");  //Adiciona um 0 caso o valor dos minutos seja <10  if (myRTC.minutes < 10)  {    Serial.print("0");  }  Serial.print(myRTC.minutes);  Serial.print(":");  //Adiciona um 0 caso o valor dos segundos seja <10  if (myRTC.seconds < 10)  {    Serial.print("0");  }  Serial.println(myRTC.seconds);  delay( 1000);}
}
