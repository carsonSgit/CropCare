from interfaces.sensors import ISensor, AReading
import random
from time import sleep


class MockAccelSensor(ISensor):
    """
    Represents an accelerometer sensor.

    Args:
        gpio (int): The GPIO pin number.
        model (str): The model of the accelerometer sensor.
        type (AReading.Type): The type of the accelerometer reading.

    Attributes:
        gpio (int): The GPIO pin number.
        model (str): The model of the accelerometer sensor.
        type (AReading.Type): The type of the accelerometer reading.

    """

    def __init__(self, gpio: int, model: str, type: AReading.Type):
        self.gpio = gpio
        self.model = model
        self.type = type

    def read_sensor(self) -> list[AReading]:
        """
        Reads the accelerometer sensor and returns a list of accelerometer readings.

        Returns:
            list[AReading]: A list of accelerometer readings.

        """

        return [
            AReading(AReading.Type.ROLL, AReading.Unit.DEGREE, random.uniform(0, 180)),
            AReading(AReading.Type.PITCH, AReading.Unit.DEGREE, random.uniform(0, 180)),
        ]


if __name__ == "__main__":
    sensor = MockAccelSensor(0, "accel", AReading.Type.PITCH)
    while True:
        readings = sensor.read_sensor()
        print("roll: " + str(readings[0]))
        print("pitch: " + str(readings[1]))
        sleep(1)
