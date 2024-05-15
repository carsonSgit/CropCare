from time import sleep
from interfaces.actuators import IActuator, ACommand


class MockFanController(IActuator):
    """
    A mock fan controller class that implements the IActuator interface.
    """

    def __init__(self, gpio: int, type: ACommand.Type, initial_state: str) -> None:
        """
        Initializes a new instance of the MockFanController class.

        Args:
            gpio (int): The GPIO pin number.
            type (ACommand.Type): The type of command.
            initial_state (str): The initial state of the fan.

        Returns:
            None
        """
        self.fan = f"mock_fan_pin_{gpio}"
        self.type = type
        self.state = initial_state

    def control_actuator(self, value: str) -> bool:
        """
        Controls the fan actuator.

        Args:
            value (str): The value to set the fan actuator to.

        Returns:
            bool: True if the state of the fan actuator changed, False otherwise.
        """
        old_state = self.state

        self.state = value.upper()
        print(f"{self.fan}: {self.state}")

        return self.state != old_state

    def validate_command(self, command: ACommand) -> bool:
        """
        Validates a command for the fan actuator.

        Args:
            command (ACommand): The command to validate.

        Returns:
            bool: True if the command is valid for the fan actuator, False otherwise.
        """
        if command.target_type == ACommand.Type.FAN:
            return command.value.upper() in ["ON", "OFF"]
        return False


fan = MockFanController(12, ACommand.Type.FAN, "OFF")
if __name__ == "__main__":
    while True:
        fan.control_actuator("ON")
        sleep(2)
        fan.control_actuator("OFF")
        sleep(2)
