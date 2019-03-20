#include "FastLED.h"
int ultraLeftTrig = 2;
int ultraLeftEcho = 3;
int ultraRightTrig = 4;
int ultraRightEcho = 5;
#define LED_COUNT 11
#define DATA_PIN 7

int distance = 0;
int duration = 0;



CRGB leds[LED_COUNT];
void setup()
{
  FastLED.addLeds<WS2812, DATA_PIN>(leds, LED_COUNT);
  pinMode(ultraLeftTrig, OUTPUT);
  pinMode(ultraLeftEcho, INPUT);

  pinMode(ultraRightTrig, OUTPUT);
  pinMode(ultraRightEcho, INPUT);
  Serial.begin(115200);

  for(int i = 0; i < LED_COUNT; i++)
  {
    //leds[i].setRGB(0, 255, 0); // GRB (nobody knows why) //joris hier idd raar dat t GRB ipv RGB heeft. maar na wat onderzeok achter gekoomen dat er geen standaard is voor ledstrips en dat de fabrikant zelg bapaalt in welke volgorde de letjes word geplaatst. 
  }
}

void loop()
{
  FastLED.show();
  SerialReader();
  SerialWriter();
  Serial.flush();

}

void SerialReader()
{
  String incomingData = Serial.readString();
  Serial.setTimeout(10);
  int value = -1;
  if(incomingData.indexOf("enable") >= 0)
  {
    value = 255;
  }
  else if (incomingData.indexOf("disable") >= 0)
  {
    value = 0;
  }
  else 
  {
    return;
  }

  leds[incomingData.toInt()].setRGB(0, value, 0);
}

void SerialWriter()
{
  Serial.print(UltraSonicSensor(ultraLeftTrig, ultraLeftEcho)); //distance left
  Serial.print(",");
  Serial.print(UltraSonicSensor(ultraRightTrig, ultraRightEcho)); // distance right
  
  for(int i = 0; i < LED_COUNT; i++)
  {
    Serial.print(",");
    Serial.print(leds[i].r);
    Serial.print("/");
    Serial.print(leds[i].g);
    Serial.print("/");
    Serial.print(leds[i].b);
  }

  Serial.println();
  delay(10);
}


int UltraSonicSensor(int triggerSensor, int echoSensor) 
{
  digitalWrite(triggerSensor, HIGH);
  delayMicroseconds(10);
  digitalWrite(triggerSensor, LOW);
  duration = pulseIn(echoSensor, HIGH);
  distance = duration * 0.017;
  //delay(100);
  return distance;
}
