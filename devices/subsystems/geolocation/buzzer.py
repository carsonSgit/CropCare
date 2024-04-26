from gpiozero import output_devices
from time import sleep
import subprocess
from interfaces.actuators import IActuator, ACommand

import seeed_python_reterminal.core as rt

class BuzzerController(IActuator):
    """
    A class representing a buzzer controller.

    Attributes:
        gpio (int): The GPIO pin number for the buzzer.
        type (ACommand.Type): The type of command for the buzzer.
        initial_state (str): The initial state of the buzzer.

    Methods:
        control_actuator(self, value: str) -> bool:
            Controls the buzzer based on the given value.
        validate_command(self, command: ACommand) -> bool:
            Validates the given command for the buzzer.

    """

    def __init__(self, gpio: int, type: ACommand.Type, initial_state: str) -> None:
        """
        Initializes the BuzzerController object.

        Args:
            gpio (int): The GPIO pin number for the buzzer.
            type (ACommand.Type): The type of command for the buzzer.
            initial_state (str): The initial state of the buzzer.

        """
        self.buzzer = rt.buzzer
        self.type = type
        self.control_actuator(initial_state)

    def control_actuator(self, value: str) -> bool:
        """
        Controls the state of the buzzer actuator.

        Args:
            value (str): The desired state of the buzzer actuator. Valid values are "ON" and "OFF".

        Returns:
            bool: True if the state of the buzzer actuator was changed, False otherwise.

        """
        old_state = self.buzzer

        if value.upper() == "ON":
            subprocess.run(
                'echo 1 | sudo tee /sys/class/leds/usr_buzzer/brightness', 
                shell=True,
                stdout=subprocess.DEVNULL
            )
        elif value.upper() == "OFF":
            subprocess.run(
                'echo 0 | sudo tee /sys/class/leds/usr_buzzer/brightness', 
                shell=True,
                stdout=subprocess.DEVNULL
            )
            

        return self.buzzer != old_state

    def validate_command(self, command: ACommand) -> bool:
        """
        Validates the given command for the buzzer control.

        Args:
            command (ACommand): The command to be validated.

        Returns:
            bool: True if the command is valid for the buzzer control, False otherwise.

        """
        if command.target_type == ACommand.Type.BUZZER:
            return command.value.upper() in ["ON", "OFF"]
        return False


if __name__ == "__main__":
    buzzer = BuzzerController(0, ACommand.Type.BUZZER, "OFF")
    while True:
        buzzer.control_actuator("ON")
        sleep(1)
        buzzer.control_actuator("OFF")
        sleep(2)