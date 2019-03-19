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
    leds[i].setRGB(0, 255, 0); // GRB (nobody knows why) //joris hier idd raar dat t GRB ipv RGb heeft. maar na wat onderzeok achter gekoomen dat er geen standaard is voor ledstrips en dat de fabrikant zelg bapaalt in welke volgorde de letjes word geplaatst. 
  }
  FastLED.show();

  Serial.flush();
  Serial.print(UltraSonicSensor(ultraLeftTrig, ultraLeftEcho)); //distance left
  Serial.print(",");
  Serial.print(UltraSonicSensor(ultraRightTrig, ultraRightEcho)); // distance right
  for(int i = 0; i < LED_COUNT; i++)
  {
    Serial.print(",");
    Serial.print(leds[i]);  
  }
  Serial.println();

  // add additional lines for heat
  delay(10);
}
