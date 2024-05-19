from random import random
from interfaces.sensors import ISensor, AReading
from time import sleep


class MockTempController(ISensor):
    """
    A mock temperature controller that reads temperature and humidity from a sensor.

    Attributes:
        temp (str): The temperature sensor bus number.
        model (str): The model of the temperature and humidity sensor.
        type (AReading.Type): The type of reading (e.g., temperature or humidity).

    """

    def __init__(self, gpio: int, model: str, type: AReading.Type):
        """
        Initialize a TempController object.

        Args:
            gpio (int): The GPIO pin number.
            model (str): The model of the temperature and humidity sensor.
            type (AReading.Type): The type of reading (e.g., temperature or humidity).

        Returns:
            None

        """
        self.temp = f"{model}_temp_sensor_busnumber_{gpio}"
        self.model = model
        self.type = type

    def read_sensor(self) -> list[AReading]:
        """
        Reads the temperature and humidity from the sensor.

        Returns:
            list[AReading]: A list of AReading objects representing the temperature and humidity readings.

        """
        return [
            AReading(AReading.Type.TEMPERATURE, AReading.Unit.CELCIUS, random() * 100),
            AReading(AReading.Type.HUMIDITY, AReading.Unit.HUMIDITY, random() * 100),
        ]


temp = MockTempController(4, "AHT20", AReading.Type.TEMPERATURE)
if __name__ == "__main__":
    while True:
        print(temp.read_sensor())
        sleep(2)
