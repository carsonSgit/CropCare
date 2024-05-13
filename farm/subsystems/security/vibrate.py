from grove import grovepi
from interfaces.actuators import IActuator, ACommand
from time import sleep


class VibrationController(IActuator):
    """
    A class representing a simulation of a vibration sensor controller.

    Attributes:
        type (ACommand.Type): The type of command supported by the simulated vibration sensor.
        callback (function): A function to execute when vibration is detected.

    Methods:
        control_actuator(self, value: str) -> bool:
            Controls the vibration sensor based on the provided value.
        validate_command(self, command: ACommand) -> bool:
            Validates the given command for the vibration sensor.
        set_callback(self, callback: function):
            Assigns a callback function for detecting vibrations.
    """

    def __init__(self, pin: int, type: ACommand.Type, callback=None) -> None:
        """
        Initializes the VibrationController object.

        Args:
            pin (int): The pin number that the vibration sensor is associated with.
            type (ACommand.Type): The type of command supported by the vibration sensor.
            callback (function): The function that will be called upon vibration detection.

        """
        self.pin = pin
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
            command (ACommand): The command that will be validated (verified).

        Returns:
            bool: True if the command is valid for the vibration sensor, False otherwise.

        """

        return command.target_type == self.type and command.value.upper() in [
            "ON",
            "OFF",
        ]

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
        grovepi.pinMode(self.pin, "INPUT")
        while True:
            try:
                value = grovepi.digitalRead(self.pin)
                if value:
                    self._handle_vibration()
                sleep(2)
            except IOError:
                print("Error")


if __name__ == "__main__":

    def callback():
        print("Alert: Vibration detected")

    vibration_sensor = VibrationController(2, ACommand.Type.VIBRATION, callback)
    vibration_sensor.start_detection()
