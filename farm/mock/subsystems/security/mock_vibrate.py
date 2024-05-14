import random
from interfaces.sensors import ISensor, AReading
from time import sleep


class MockVibrationSensor(ISensor):
    """
    A class representing a simulation of a vibration sensor.

    Attributes:
        model (str): The model of the vibration sensor.
        type (AReading.Type): The type of the sensor reading.
        callback (function): A function to execute when vibration is detected.

    Methods:
        read_sensor(): Reads the vibration value from the sensor.
        set_callback(callback): Assigns a callback function for detecting simulated vibrations.
        start_detection(): Starts the vibration detection.
    """

    def __init__(self, model: str, type: AReading.Type, callback=None) -> None:
        """
        Initializes the MockVibrationSensor.

        Args:
            model (str): The model of the vibration sensor.
            type (AReading.Type): The type of reading the sensor produces.
            callback (optional): A callback function that will be called when a vibration is detected.
        """
        self.model = model
        self.type = type
        self.callback = callback

    def read_sensor(self) -> bool:
        """
        Reads the vibration value from the sensor.

        Returns:
            bool: True if vibration is detected, False otherwise.
        """
        # Simulate a vibration reading with a 70% chance of detecting vibration
        detected = random.random() > 0.7
        print(detected)  # Print True or False
        return detected

    def set_callback(self, callback):
        """
        Sets the controller's callback function that will be called upon vibration detection.

        Args:
            callback: The callback function.
        """
        self.callback = callback

    def _handle_vibration(self):
        """
        Handles the vibration detection by calling the given callback function.
        """
        if self.callback:
            self.callback()

    def start_detection(self):
        """
        Starts the vibration detection.

        As this is mock, vibration will be determined as "detected" on a random chance.
        """
        while True:
            try:
                if self.read_sensor():
                    self._handle_vibration()
                sleep(2)
            except KeyboardInterrupt:
                break


if __name__ == "__main__":

    def callback():
        print("Alert: Vibration detected")

    mock_vibration_sensor = MockVibrationSensor("MockVibrationSensor", AReading.Type.MOTION, callback)
    mock_vibration_sensor.start_detection()
