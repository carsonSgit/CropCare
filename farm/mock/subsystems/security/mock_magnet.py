from interfaces.sensors import ISensor, AReading
import time
import math
import random


class MockMagnetSensor(ISensor):
    """
    Represents a magnet sensor that detects the presence of a magnetic field.

    Args:
        gpio (int): The GPIO pin number to which the sensor is connected.
        model (str): The model of the magnet sensor.
        type (AReading.Type): The type of reading produced by the sensor.

    Attributes:
        button (Button): The button object representing the physical button connected to the sensor.
        model (str): The model of the magnet sensor.
        type (AReading.Type): The type of reading produced by the sensor.
    """

    def __init__(self, gpio: int, model: str, type: AReading.Type):
        self.gpio = gpio
        self.model = model
        self.type = type

    def read_sensor(self) -> AReading:
        """
        Reads the sensor and returns the reading.

        Returns:
            AReading: The reading produced by the sensor.
        """
        is_active = True if random.random() > 0.5 else False

        return [AReading(AReading.Type.MAGNET, AReading.Unit.NONE, is_active)]


if __name__ == "__main__":
    magnet = MockMagnetSensor(24, "MagnetSesnor", AReading.Type.MAGNET)
    while True:
        magnet_reading = magnet.read_sensor()
        print(magnet_reading)
        time.sleep(1)
