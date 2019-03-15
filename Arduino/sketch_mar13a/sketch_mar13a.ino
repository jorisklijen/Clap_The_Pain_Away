#include "FastLED.h"
int ultraLeftTrig = 2;
int ultraLeftEcho = 3;
int ultraRightTrig = 4;
int ultraRightEcho = 5;
#define LED_COUNT 10
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
}

int UltraSonicSensor(int triggerSensor, int echoSensor) {
  digitalWrite(triggerSensor, HIGH);
  delayMicroseconds(10);
  digitalWrite(triggerSensor, LOW);
  duration = pulseIn(echoSensor, HIGH);
  distance = duration * 0.017;
  //delay(100);
  return distance;
}

void loop() 
{
  for(int i = 0; i < LED_COUNT; i++)
  {
    leds[i].setRGB(0, 255, 0); // GRB (nobody knows why)
  }
  FastLED.show();
  
  Serial.flush();
  Serial.print(UltraSonicSensor(ultraLeftTrig, ultraLeftEcho)); //distance left
  Serial.print(",");
  Serial.print(UltraSonicSensor(ultraRightTrig, ultraRightEcho)); // distance right
  Serial.print(",");
  Serial.print(82); // audio decibel level
  Serial.println();

  // add additional lines for heat
  delay(10);
}
