import random
from interfaces.sensors import ISensor, AReading
from time import sleep
from typing import List


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

    def __init__(self, gpio: int, model: str, type: AReading.Type) -> None:
        """
        Initializes the MockVibrationSensor.

        Args:
            model (str): The model of the vibration sensor.
            type (AReading.Type): The type of reading the sensor produces.
            callback (optional): A callback function that will be called when a vibration is detected.
        """
        self.model = model
        self.type = type

    def read_sensor(self) -> List[AReading]:
        """
        Reads the vibration value from the sensor.

        Returns:
            bool: True if vibration is detected, False otherwise.
        """
        # Simulate a vibration reading with a 70% chance of detecting vibration
        detected = random.random() > 0.7
        return [AReading(AReading.Type.VIBRATION, AReading.Unit.NONE, detected)]


if __name__ == "__main__":
    mock_vibration_sensor = MockVibrationSensor(
        26, "MockVibrationSensor", AReading.Type.MOTION
    )
