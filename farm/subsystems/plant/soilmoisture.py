from interfaces.sensors import ISensor, AReading
from grove.adc import ADC
import grove.i2c
from time import sleep


class customADC(ADC):
    def __init__(self, address=0x04, bus=1):
        self.address = address
        self.bus = grove.i2c.Bus(bus)


class SoilMoistureSensor(ISensor):
    """
    A sensor class for reading Soil Moisture

    Implements the ISensor interface

    Attributes:
        gpio (int): Bus number that the deice is connected to
        model (str): The model of the soil moisture controller.
        type (AReading.Type): The type of the sensor reading.

    Methods:
        read_sensor(): Reads the soil moisture from the sensor.

    """

    def __init__(self, gpio: int, model: str, type: AReading.Type):
        """
        Initialize a reTerminal sensor object.

        Args:
            gpio (int): The GPIO pin number.
            model (str): The model of the soil moisture sensor.
            type (AReading.Type): The type of reading (e.g., moisture).

        Returns:
            None

        """
        self.gpio = gpio
        self.model = model
        self.type = type
        self.adc = customADC()

    def read_sensor(self) -> list[AReading]:
        """
        Takes a reading from the soil moisture sensor.

        Args: none

        Returns:
            list[AReading]: a list of soil moisture readings.

        """
        soil_moisture = self.adc.read(self.gpio)

        return [AReading(AReading.Type.MOISTURE, AReading.Unit.OHMS, soil_moisture)]


temp = SoilMoistureSensor(0, "Grove Moisuture Reader", AReading.Type.MOISTURE)
if __name__ == "__main__":
    while True:
        print(str(temp.read_sensor()))
        sleep(1)
