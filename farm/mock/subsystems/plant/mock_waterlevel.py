from interfaces.sensors import ISensor, AReading
from random import random
from time import sleep


class MockWaterLevelSensor(ISensor):
    """
    A sensor class for reading water level

    Implements the ISensor interface

    Attributes:
        gpio (int): Bus number that the deice is connected to
        model (str): The model of the water level controller.
        type (AReading.Type): The type of the sensor reading.

    Methods:
        read_sensor(): Reads the water level from the sensor.

    """

    def __init__(self, gpio: int, model: str, type: AReading.Type):
        """
        Initialize a reTerminal sensor object.

        Args:
            gpio (int): The GPIO pin number.
            model (str): The model of the water level sensor.
            type (AReading.Type): The type of reading (e.g., water level).

        Returns:
            None

        """
        self.gpio = gpio
        self.model = model
        self.type = type

    def read_sensor(self) -> list[AReading]:
        """
        Takes a reading from the water level sensor.

        Args: none

        Returns:
            list[AReading]: a list of water level readings.

        """
        water_level = random() * 100
        return [
            AReading(AReading.Type.WATERLEVEL, AReading.Unit.WATERLEVEL, water_level)
        ]
