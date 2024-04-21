import time
import random
from interfaces.sensors import ISensor, AReading

class MockGPSSensor(ISensor):
    """
    A mock class representing a GPS sensor.

    Attributes:
        model (str): The model of the GPS sensor.
        type (AReading.Type): The type of the sensor reading.

    Methods:
        generate_random_reading(): Generates random latitude and longitude values.
        read_sensor(): Simulates reading the GPS data from the sensor.

    """

    def __init__(self, model: str, type: AReading.Type):
        """
        Initialize a GPSSensor object.

        Args:
            model (str): The model of the GPS sensor.
            type (AReading.Type): The type of reading (e.g., latitude or longitude).

        Returns:
            None

        """
        self.model = model
        self.type = type

    def generate_random_reading(self) -> tuple:
        """
        Generates random latitude and longitude values.

        Returns:
            tuple: A tuple containing latitude (float) and longitude (float).

        """
        latitude = round(random.uniform(-90, 90), 6)
        longitude = round(random.uniform(-180, 180), 6)
        return latitude, longitude

    def read_sensor(self) -> AReading:
        """
        Simulates reading the GPS data from the sensor.

        Returns:
            AReading: An AReading object representing the GPS data.

        """
        latitude, longitude = self.generate_random_reading()
        return [
            AReading(AReading.Type.LATITUDE, AReading.Unit.DEGREE, latitude),
            AReading(AReading.Type.LONGITUDE, AReading.Unit.DEGREE, longitude)
        ]

gps = MockGPSSensor("Mock GPS", AReading.Type.GPS)

if __name__ == "__main__":
    while True:
        gps_reading = gps.read_sensor()
        print(gps_reading)
        time.sleep(1)
