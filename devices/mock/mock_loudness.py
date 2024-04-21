import random
import time
from interfaces.sensors import ISensor, AReading

class MockLoudnessSensor(ISensor):
    """
    A mock class representing a loudness sensor.

    Attributes:
        model (str): The model of the mock loudness sensor.
        type (AReading.Type): The type of the sensor reading.

    Methods:
        read_sensor(): Generates a random loudness value.

    """

    def __init__(self, model: str, type: AReading.Type):
        """
        Initialize a MockLoudnessSensor object.

        Args:
            model (str): The model of the mock loudness sensor.
            type (AReading.Type): The type of reading (e.g., loudness).

        Returns:
            None

        """
        self.model = model
        self.type = type

    def read_sensor(self) -> AReading:
        """
        Generates a random loudness value.

        Returns:
            AReading: An AReading object representing the mock loudness reading.

        """
        loudness = random.randint(0, 100)
        return AReading(self.type, None, loudness)

mock_loudness_sensor = MockLoudnessSensor("MockLoudnessSensor", AReading.Type.LOUDNESS)

if __name__ == "__main__":
    while True:
        loudness_reading = mock_loudness_sensor.read_sensor()
        print("Mock Loudness value: {}dB".format(loudness_reading.value))
        time.sleep(1)
