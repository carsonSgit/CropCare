import time
import serial
from interfaces.sensors import ISensor, AReading

import pynmea2


class GPS(ISensor):
    """
    A class representing a GPS sensor.

    Attributes:
        port (str): The serial port the GPS is connected to.
        baudrate (int): The baudrate for the serial communication.
        type (AReading.Type): The type of the sensor reading.

    Methods:
        read_sensor(): Reads the GPS data from the sensor.

    """

    def __init__(self, port: str, baudrate: int, type: AReading.Type):
        """
        Initialize a GPS object.

        Args:
            port (str): The serial port the GPS is connected to.
            baudrate (int): The baudrate for the serial communication.
            type (AReading.Type): The type of reading (e.g., GPS).

        Returns:
            None

        """
        self.port = port
        self.baudrate = baudrate
        self.type = type
        self.serial = serial.Serial(port, baudrate, timeout=1)
        self.serial.reset_input_buffer()
        self.serial.flush()

    def read_sensor(self) -> list[AReading]:
        """
        Reads the GPS data from the sensor.

        Returns:
            list[AReading]: A list containing GPS readings.

        """
        try:
            line = self.serial.readline().decode("utf-8")
            return self.parse_gps_data(line)
        except UnicodeDecodeError:
            print("UnicodeDecodeError encountered")
            return []

    def parse_gps_data(self, line: str) -> list[AReading]:
        """
        Parses GPS data from a NMEA sentence.

        Args:
            line (str): A string containing NMEA data.

        Returns:
            list[AReading]: A list containing GPS readings.

        """
        readings = []
        if line.find("GGA") > 0:
            try:
                msg = pynmea2.parse(line)
                readings.extend(
                    [
                        AReading(
                            AReading.Type.LATITUDE, AReading.Unit.DEGREE, msg.latitude
                        ),
                        AReading(
                            AReading.Type.LONGITUDE, AReading.Unit.DEGREE, msg.longitude
                        ),
                    ]
                )
            except pynmea2.ParseError as e:
                print(f"Parse error: {e}")
        return readings


if __name__ == "__main__":
    # Plug in GPS sensor into UART port on Base Hat
    gps = GPS("/dev/ttyS0", 9600, AReading.Type.GPS)
    while True:
        gps_reading = gps.read_sensor()
        if gps_reading:
            for reading in gps_reading:
                print(reading)
        time.sleep(1)
