from time import sleep
from interfaces.actuators import IActuator, ACommand


class MockBuzzerController(IActuator):
    """
    A mock buzzer controller class that implements the IActuator interface.
    """

    def __init__(self, gpio: int, type: ACommand.Type, initial_state: str) -> None:
        """
        Initializes a new instance of the MockBuzzerController class.

        Args:
            gpio (int): The GPIO pin number.
            type (ACommand.Type): The type of command.
            initial_state (str): The initial state of the buzzer.

        Returns:
            None
        """
        self.buzzer = f"mock_buzzer_pin_{gpio}"
        self.type = type
        self.__state = initial_state

    def control_actuator(self, value: str) -> bool:
        """
        Controls the buzzer actuator.

        Args:
            value (str): The value to set the buzzer actuator to.

        Returns:
            bool: True if the state of the buzzer actuator changed, False otherwise.
        """
        old_state = self.__state

        self.__state = value.upper()
        print(f"{self.buzzer}: {self.__state}")

        return self.__state != old_state

    def validate_command(self, command: ACommand) -> bool:
        """
        Validates a command for the buzzer actuator.

        Args:
            command (ACommand): The command to validate.

        Returns:
            bool: True if the command is valid for the buzzer actuator, False otherwise.
        """
        if command.target_type == ACommand.Type.BUZZER:
            return command.value.upper() in ["ON", "OFF"]
        return False


buzzer = MockBuzzerController(12, ACommand.Type.BUZZER, "OFF")
if __name__ == "__main__":
    while True:
        buzzer.control_actuator("ON")
        sleep(2)
        buzzer.control_actuator("OFF")
        sleep(2)