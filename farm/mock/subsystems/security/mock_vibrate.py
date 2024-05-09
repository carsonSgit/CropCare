import random
from interfaces.actuators import IActuator, ACommand
from time import sleep


class MockVibrationController(IActuator):
    """
    A class representing a simulation of a vibration sensor controller.

    Attributes:
        type (ACommand.Type): The type of command supported by the simulated vibration sensor.
        callback (function): A function to execute when vibration is detected.

    Methods:
        control_actuator(self, value: str) -> bool:
            Controls the simulated vibration sensor based on the provided value. Not utilized.
        validate_command(self, command: ACommand) -> bool:
            Validates the given command for the simulated vibration sensor.
        set_callback(self, callback: function):
            Assigns a callback function for detecting simulated vibrations.

    """

    def __init__(self, type: ACommand.Type, callback=None) -> None:
        """
        Initializes the MockVibrationController.

        Args:
            type (ACommand.Type): The type of command the controller will handle.
            callback (optional): A callback function that will be called when a vibration is detected.
        """
        self.type = type
        self.callback = callback

    def control_actuator(self, value: str) -> bool:
        """
        Controls the actuator based on the provided value.

        Args:
            value (str): The value to control the actuator.

        Returns:
            bool: True as there is no control for the vibration sensor.
        """
        return True

    def validate_command(self, command: ACommand) -> bool:
        """
        Validates the command based on its target type and value.

        Args:
            command (ACommand): The command that will be validated.

        Returns:
            bool: True if given a valid command, False otherwise.
        """
        return command.target_type == self.type and command.value.upper() in [
            "ON",
            "OFF",
        ]

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

        As this is mock, it be determined as "detected" on random chance.
        """
        while True:
            try:
                if random.random() > 0.7:  # 70% chance
                    self._handle_vibration()
                sleep(2)
            except KeyboardInterrupt:
                break


if __name__ == "__main__":

    def callback():
        print("Alert: Vibration detected")

    mock_vibration_sensor = MockVibrationController(ACommand.Type.VIBRATION, callback)
    mock_vibration_sensor.start_detection()
