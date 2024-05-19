from interfaces.actuators import IActuator, ACommand
from time import sleep


class MockLEDController(IActuator):
    """
    Represents a mock LED controller.

    This class implements the IActuator interface and provides methods to control an LED actuator.
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
        self.led = f"mock_led_pin_{gpio}"
        self.type = type
        self.state = initial_state

    def control_actuator(self, value: str) -> bool:
        """
        Controls the actuator based on the given value.

        Args:
            value (str): The value to control the actuator.

        Returns:
            bool: True if the state of the actuator has changed, False otherwise.

        """
        old_state = self.state

        self.state = value.upper()
        print(f"{self.led}: {self.state}")

        return old_state != self.state

    def validate_command(self, command: ACommand) -> bool:
        """
        Validates if the given command is supported by the LED controller.

        Args:
            command (ACommand): The command to validate.

        Returns:
            bool: True if the command is valid, False otherwise.

        """
        if command.target_type in [
            ACommand.Type.LIGHT_ON_OFF,
            ACommand.Type.LIGHT_PULSE,
        ]:
            return command.value.upper() in ["ON", "OFF"]
        return False


if __name__ == "__main__":
    led = MockLEDController(16, ACommand.Type.LIGHT_PULSE, "OFF")
    while True:
        led.control_actuator("ON")
        sleep(2)
        led.control_actuator("OFF")
        sleep(2)
