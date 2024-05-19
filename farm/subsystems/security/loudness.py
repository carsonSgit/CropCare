from grove.grove_loudness_sensor import GroveLoudnessSensor
from grove.adc import ADC
import grove.i2c
from interfaces.sensors import ISensor, AReading
import time
import math


class customADC(ADC):
    """
    A Class to create a custom ADC module to run our Loudness Sensor
    """

    def __init__(self, address=0x04, bus=1):
        self.address = address
        self.bus = grove.i2c.Bus(bus)


class LoudnessSensor(ISensor):
    """
    A class representing a loudness sensor.

    Attributes:
        channel (int): The analog pin/channel the sensor is connected to.
        model (str): The model of the loudness sensor.
        type (AReading.Type): The type of the sensor reading.

    Methods:
        read_sensor(): Reads the loudness value from the sensor.

    """

    def __init__(self, channel: int, model: str, type: AReading.Type):
        """
        Initialize a LoudnessSensor object.

        Args:
            channel (int): The analog pin/channel the sensor is connected to.
            model (str): The model of the loudness sensor.
            type (AReading.Type): The type of reading (e.g., loudness).

        Returns:
            None

        """
        self.channel = channel
        self.model = model
        self.type = type
        self.adc = customADC()

    def read_sensor(self) -> AReading:
        """
        Reads the loudness value from the sensor.

        Returns:
            AReading: An AReading object representing the loudness reading.

        """
        analog = self.adc.read(self.channel)
        level = self._analog_to_string(analog)

        return [AReading(AReading.Type.LOUDNESS, AReading.Unit.NONE, level)]

    def _analog_to_string(self, analog: int) -> str:
        """
        Converts an analog value to a corresponding loudness level.

        Args:
            analog (int): The analog value to be converted.

        Returns:
            str: The corresponding loudness level based on the analog value.
        """
        level = ""
        if analog > 682:
            level = "Quiet"
        elif analog > 341:
            level = "Noisy"
        else:
            level = "Loud"
        return level


if __name__ == "__main__":
    loudness = LoudnessSensor(0, "LoudnessSensor", AReading.Type.LOUDNESS)
    while True:
        loudness_reading = loudness.read_sensor()
        print(loudness_reading)
        time.sleep(1)
