import time
import serial
import pynmea2
from interfaces.sensors import ISensor, AReading

class GPSSensor(ISensor):
    """
    A class representing a GPS sensor.

    Attributes:
        model (str): The model of the GPS sensor.
        type (AReading.Type): The type of the sensor reading.

    Methods:
        read_sentence(): Reads the GPS data from the sensor.

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
        self.serial = serial.Serial('/dev/ttyS0', 9600, timeout=1)
        self.serial.reset_input_buffer()
        self.serial.flush()

    def read_sentence(self) -> str:
        """
        Reads a NMEA sentence from the GPS sensor.

        Returns:
            str: A string containing a NMEA sentence.

        """
        line = self.serial.readline().decode('utf-8').strip()
        print("Received:", line)
        return line

    def parse_gpgga(self, sentence: str) -> tuple:
        """
        Parses GPGGA sentence to extract latitude and longitude.

        Args:
            sentence (str): A string containing a GPGGA NMEA sentence.

        Returns:
            tuple: A tuple containing latitude (float) and longitude (float).

        """
        try:
            msg = pynmea2.parse(sentence)
            latitude = msg.latitude
            longitude = msg.longitude
            return latitude, longitude
        except pynmea2.ParseError as e:
            print(f"Error parsing NMEA sentence: {e}")
            return None, None


    def read_sensor(self) -> AReading:
        """
        Reads the GPS data from the sensor.

        Returns:
            AReading: An AReading object representing the GPS data.

        """
        while True:
            sentence = self.read_sentence()
            if sentence.startswith('$GPGGA'):
                latitude, longitude = self.parse_gpgga(sentence)
                return [
                    AReading(AReading.Type.LATITUDE, AReading.Unit.DEGREE, latitude),
                    AReading(AReading.Type.LONGITUDE, AReading.Unit.DEGREE, longitude)
                ]

gps = GPSSensor("GPS", AReading.Type.GPS)

if __name__ == "__main__":
    while True:
        gps_reading = gps.read_sensor()
        print(gps_reading)
        time.sleep(1)
