from grove.grove_loudness_sensor import GroveLoudnessSensor
from interfaces.sensors import ISensor, AReading
import time

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
        self.loudness_sensor = GroveLoudnessSensor(channel)
        self.model = model
        self.type = type

    def read_sensor(self) -> AReading:
        """
        Reads the loudness value from the sensor.

        Returns:
            AReading: An AReading object representing the loudness reading.

        """
        loudness = self.loudness_sensor.value
        return AReading(self.type, None, loudness)

loudness = LoudnessSensor(4, "LoudnessSensor", AReading.Type.LOUDNESS)

if __name__ == "__main__":
    while True:
        loudness_reading = loudness.read_sensor()
        print("Loudness value: {}".format(loudness_reading.value))
        time.sleep(1)
