from interfaces.sensors import ISensor, AReading
import seeed_python_reterminal.core as rt
from time import sleep


class LuminositySensor(ISensor):
    """
    A sensor class for reading Luminosity levels from the reTerminal.

    Implements the ISensor interface

    Attributes:
        gpio (int): I'm actually using this as the bus number...
        model (str): The model of the temperature controller.
        type (AReading.Type): The type of the sensor reading.

    Methods:
        read_sensor(): Reads the luminosity level from the reTerminal.

    """

    def __init__(self, gpio: int, model: str, type: AReading.Type):
        """
        Initialize a reTerminal sensor object.

        Args:
            gpio (int): The GPIO pin number (as this is internal, it is actually the bus).
            model (str): The model of the luminosity sensor.
            type (AReading.Type): The type of reading (e.g., luminosity).

        Returns:
            None

        """
        self.gpio = gpio
        self.model = model
        self.type = type

    def read_sensor(self) -> list[AReading]:
        """
        Takes a reading from the luminosity sensor.

        Args: none

        Returns:
            list[AReading]: a list of luminosity readings.

        """
        luminosity = rt.illuminance
        return [
            AReading(AReading.Type.LUMINOSITY, AReading.Unit.LUX, luminosity),
        ]
