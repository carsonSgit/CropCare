from time import sleep
from gpiozero import Servo
from interfaces.actuators import IActuator, ACommand


class ServoController(IActuator):
    """
    A class representing a servo controller.

    Attributes:
        servo (Servo): The servo object used for controlling the servo motor.
        type (ACommand.Type): The type of command that the servo controller accepts.
    """

    def __init__(self, gpio: int, type: ACommand.Type, initial_state: str) -> None:
        self.servo = Servo(gpio)
        self.type = type
        self.value = initial_state
        self.control_actuator(initial_state)

    def control_actuator(self, value: str) -> bool:
        """
        Control the servo motor by setting its value.

        Args:
            value (str): The value to set for the servo motor.

        Returns:
            bool: True if the value was successfully set, False otherwise.
        """
        old_state = self.servo.value
        self.value = float(value)
        self.servo.value = float(value)

        return old_state != self.servo.value

    def validate_command(self, command: ACommand) -> bool:
        """
        Validate a command for the servo controller.

        Args:
            command (ACommand): The command to validate.

        Returns:
            bool: True if the command is valid, False otherwise.
        """
        if command.target_type == self.type:
            return -1 <= float(command.value) <= 1
        return False


if __name__ == "__main__":
    servo = ServoController(16, ACommand.Type.SERVO, "-1")
    while True:
        print(servo.value)
        servo.control_actuator("-1")
        sleep(1)
        print(servo.value)
        servo.control_actuator(0)
        sleep(1)
        print(servo.value)
        servo.control_actuator(1)
        sleep(1)
