from grove import grovepi
from interfaces.sensors import ISensor, AReading
from time import sleep


class VibrationSensor(ISensor):
    """
    A class representing a vibration sensor.

    Attributes:
        pin (int): The pin number that the vibration sensor is associated with.
        model (str): The model of the vibration sensor.
        type (AReading.Type): The type of the sensor reading.
        callback (function): A function to execute when vibration is detected.

    Methods:
        read_sensor(): Reads the vibration value from the sensor.
        set_callback(callback): Assigns a callback function for detecting vibrations.
        start_detection(): Starts the vibration detection.
    """

    def __init__(self, pin: int, model: str, type: AReading.Type, callback=None) -> None:
        """
        Initializes the VibrationSensor object.

        Args:
            pin (int): The pin number that the vibration sensor is associated with.
            model (str): The model of the vibration sensor.
            type (AReading.Type): The type of the sensor reading.
            callback (function): The function that will be called upon vibration detection.
        """
        self.pin = pin
        self.model = model
        self.type = type
        self.callback = callback
        grovepi.pinMode(self.pin, "INPUT")

    def read_sensor(self) -> bool:
        """
        Reads the vibration value from the sensor.

        Returns:
            bool: True if vibration is detected, False otherwise.
        """
        try:
            value = grovepi.digitalRead(self.pin)
            detected = bool(value)
            print(detected)  # Print True or False
            return detected
        except IOError:
            print("Error reading from vibration sensor")
            return False

    def set_callback(self, callback):
        """
        Sets a callback function for the vibration detection.

        Args:
            callback: The function that will be called upon vibration detection.
        """
        self.callback = callback

    def _handle_vibration(self):
        """
        Internal function to handle the vibration detection.
        """
        if self.callback:
            self.callback()

    def start_detection(self):
        """
        Starts the vibration detection.
        """
        while True:
            if self.read_sensor():
                self._handle_vibration()
            sleep(2)


if __name__ == "__main__":
    def callback():
        print("Alert: Vibration detected")

    vibration_sensor = VibrationSensor(26, "VibrationSensor", AReading.Type.MOTION, callback)
    vibration_sensor.start_detection()
