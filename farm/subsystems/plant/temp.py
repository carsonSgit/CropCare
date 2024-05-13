from grove.grove_temperature_humidity_aht20 import GroveTemperatureHumidityAHT20
from interfaces.sensors import ISensor, AReading


class TempController(ISensor):
    """
    A class representing a temperature controller sensor.

    Attributes:
        gpio (int): The GPIO pin number.
        model (str): The model of the temperature controller.
        type (AReading.Type): The type of the sensor reading.

    Methods:
        read_sensor(): Reads the temperature and humidity from the sensor.

    """

    def __init__(self, gpio: int, model: str, type: AReading.Type):
        """
        Initialize a TempHumiSensor object.

        Args:
            gpio (int): The GPIO pin number.
            model (str): The model of the temperature and humidity sensor.
            type (AReading.Type): The type of reading (e.g., temperature or humidity).

        Returns:
            None

        """
        self.temp = GroveTemperatureHumidityAHT20(bus=gpio)
        self.model = model
        self.type = type

    def read_sensor(self) -> list[AReading]:
        """
        Reads the temperature and humidity from the sensor.

        Returns:
            list[AReading]: A list of AReading objects representing the temperature and humidity readings.

        """
        temp, humid = self.temp.read()

        return [
            AReading(AReading.Type.TEMPERATURE, AReading.Unit.CELCIUS, temp),
            AReading(AReading.Type.HUMIDITY, AReading.Unit.HUMIDITY, humid),
        ]


temp = TempController(4, "AHT20", AReading.Type.TEMPERATURE)
if __name__ == "__main__":
    while True:
        temperature, _ = temp.read_sensor()
