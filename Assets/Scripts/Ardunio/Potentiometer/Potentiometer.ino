/*
  ReadAnalogVoltage

  Reads an analog input on pin 0, converts it to voltage, and prints the result
  to the Serial Monitor. Graphical representation is available using Serial
  Plotter (Tools > Serial Plotter menu). Attach the center pin of a
  potentiometer to pin A0, and the outside pins to +5V and ground.

  This example code is in the public domain.

  http://www.arduino.cc/en/Tutorial/ReadAnalogVoltage
*/

int deltaAngle;
int previousAngle;

// the setup routine runs once when you press reset:
void setup() {
    // initialize serial communication at 9600 bits per second:
    Serial.begin(9600);

    deltaAngle    = 0;
    previousAngle = 0;
}

// the loop routine runs over and over again forever:
void loop() {
    // read the input on analog pin 0:
    int sensorValue = analogRead(A0);
    // Convert the analog reading (which goes from 0 - 1023) to a voltage (0 -
    // 5V):
    float voltage = sensorValue * (5.0 / 1023.0);
    // print out the value you read:
    float angle = voltage * 54;

    int intAngle = (int)angle;

    if (intAngle != previousAngle) {
        // SerialUSB.println(intAngle);
        // SerialUSB.println(previousAngle);

        deltaAngle = intAngle - previousAngle;
        previousAngle = intAngle;
    }

    SerialUSB.println(deltaAngle);
    // doesn't necessarily need write function since Serial.print() and Serial.println() statements send data the same place as Serial.write()
    // SerialUSB.write() might cause weird characters
    //SerialUSB.write(deltaAngle);
    SerialUSB.flush(); // for completing previous data sending
    delay(100); // in case the previous line doesn't work well
}
