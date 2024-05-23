from gpiozero import output_devices
from time import sleep
from interfaces.actuators import IActuator, ACommand


class FanController(IActuator):
    """
    A class representing a fan controller.

    Attributes:
        gpio (int): The GPIO pin number for the fan.
        type (ACommand.Type): The type of command for the fan.
        initial_state (str): The initial state of the fan.

    Methods:
        control_actuator(self, value: str) -> bool:
            Controls the fan based on the given value.
        validate_command(self, command: ACommand) -> bool:
            Validates the given command for the fan.

    """

    def __init__(self, gpio: int, type: ACommand.Type, initial_state: str) -> None:
        """
        Initializes the FanController object.

        Args:
            gpio (int): The GPIO pin number for the fan.
            type (ACommand.Type): The type of command for the fan.
            initial_state (str): The initial state of the fan.

        """
        self.fan = output_devices.OutputDevice(gpio)
        self.type = type
        self.state = initial_state
        self.control_actuator(initial_state)

    def control_actuator(self, value: str) -> bool:
        """
        Controls the state of the fan actuator.

        Args:
            value (str): The desired state of the fan actuator. Valid values are "ON" and "OFF".

        Returns:
            bool: True if the state of the fan actuator was changed, False otherwise.

        """
        old_state = self.fan.is_active
        if value.upper() == "ON":
            self.fan.on()
        elif value.upper() == "OFF":
            self.fan.off()
        self.state = value.upper()
        print("FAN: " + self.state)
        return self.state != old_state

    def validate_command(self, command: ACommand) -> bool:
        """
        Validates the given command for the fan control.

        Args:
            command (ACommand): The command to be validated.

        Returns:
            bool: True if the command is valid for the fan control, False otherwise.

        """
        if command.target_type == ACommand.Type.FAN:
            return command.value.upper() in ["ON", "OFF"]
        return False


if __name__ == "__main__":
    fan = FanController(12, ACommand.Type.FAN, "OFF")
    while True:
        fan.control_actuator("ON")
        sleep(2)
        fan.control_actuator("OFF")
        sleep(2)
