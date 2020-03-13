#include <SPI.h>
#include <SD.h>

File myFile;

void setup()
{
  if (!SD.begin(6))
  {
    // falhou
  }
  else
  {
    SD.remove("teste.txt"); 
    myFile = SD.open("teste.txt", FILE_WRITE);

    myFile.print("09/03 - 02940 034929030 04023\n");
    myFile.print("08/03 - 42335 034929030 04023\n");
    myFile.print("07/03 - 12467 034929030 04023\n");
    myFile.print("06/03 - 02940 034929030 04023\n");
    
    myFile.close();
    return;
  }
}

void loop()
{
  // nothing happens after setup
}
