from interfaces.sensors import ISensor, AReading
from grove.adc import ADC
import grove.i2c
from time import sleep


class customADC(ADC):
    def __init__(self, address=0x04, bus=1):
        self.address = address
        self.bus = grove.i2c.Bus(bus)


class WaterLevelSensor(ISensor):
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
        self.adc = customADC()

    def read_sensor(self) -> list[AReading]:
        """
        Takes a reading from the water level sensor.

        Args: none

        Returns:
            list[AReading]: a list of water level readings.

        """
        water_level = self.adc.read(self.gpio)

        return [
            AReading(AReading.Type.WATERLEVEL, AReading.Unit.OHMS, water_level)
        ]


temp = WaterLevelSensor(5, "Grove Moisuture Reader", AReading.Type.TEMPERATURE)
if __name__ == "__main__":
    while True:
        print(str(temp.read_sensor()))
        sleep(1)
