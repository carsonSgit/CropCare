from gpiozero import PWMLED
from actuators import IActuator, ACommand


class LEDController(IActuator):
    """
    A class representing an LED controller.

    Args:
        gpio (int): The GPIO pin number for the LED.
        type (ACommand.Type): The type of command supported by the LED controller.
        initial_state (str): The initial state of the LED.

    Attributes:
        led (PWMLED): The PWMLED object representing the LED.
        type (ACommand.Type): The type of command supported by the LED controller.

    Methods:
        control_actuator: Controls the actuator based on the given value.
        validate_command: Validates if the given command is supported by the LED controller.

    """

    def __init__(self, gpio: int, type: ACommand.Type, initial_state: str) -> None:
        """
        Initialize the LED PWM object.

        Args:
            gpio (int): The GPIO pin number for the LED.
            type (ACommand.Type): The type of command.
            initial_state (str): The initial state of the LED.

        Returns:
            None

        """
        self.led = PWMLED(gpio)
        self.type = type
        self.control_actuator(initial_state)

    def control_actuator(self, value: str) -> bool:
        """
        Controls the actuator based on the given value.

        Args:
            value (str): The value to control the actuator.

        Returns:
            bool: True if the state of the actuator has changed, False otherwise.

        """
        old_state = self.led.is_active

        if self.type == ACommand.Type.LIGHT_PULSE:
            self.__pulse_mode(value)
        elif self.type == ACommand.Type.LIGHT_ON_OFF:
            self.__on_off(value)

        return old_state != self.led.is_active

    def __on_off(self, value: str) -> None:
        """
        Turns the LED on or off based on the given value.

        Args:
            value (str): The value to turn the LED on or off.

        """
        if value.upper() == "ON":
            self.led.on()
        elif value.upper() == "OFF":
            self.led.off()

    def __pulse_mode(self, value: str) -> None:
        """
        Sets the LED to pulse mode or turns it off based on the given value.

        Args:
            value (str): The value to set the LED to pulse mode or turn it off.

        """
        if value.upper() == "ON":
            self.led.pulse()
        elif value.upper() == "OFF":
            self.led.off()

    def validate_command(self, command: ACommand) -> bool:
        """
        Validates if the given command is supported by the LED controller.

        Args:
            command (ACommand): The command to validate.

        Returns:
            bool: True if the command is valid, False otherwise.

        """
        if command.target_type == self.type:
            return command.value.upper() in ["ON", "OFF"]
        return False


if __name__ == "__main__":
    led = LEDController(16, ACommand.Type.LIGHT_PULSE, "OFF")
    led.control_actuator("ON")
    while True:
        print(led.led.is_active)