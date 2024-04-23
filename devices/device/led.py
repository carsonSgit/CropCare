from interfaces.actuators import IActuator, ACommand
from grove.grove_ws2813_rgb_led_strip import GroveWS2813RgbStrip
from rpi_ws281x import Color
import time

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
        self.led = GroveWS2813RgbStrip(gpio, 30)
        self.type = type
        self.state = initial_state
        self.control_actuator(initial_state)

    def control_actuator(self, value: str) -> bool:
        """
        Controls the actuator based on the given value.

        Args:
            value (str): The value to control the actuator.

        Returns:
            bool: True if the state of the actuator has changed, False otherwise.

        """
        old_state = self.state

        if self.type == ACommand.Type.LIGHT_ON_OFF:
            self.__on_off(value)

        return old_state != value

    def __on_off(self, value: str) -> None:
        """
        Turns the LED on or off based on the given value.

        Args:
            value (str): The value to turn the LED on or off.

        """
        if value.upper() == "ON":
            for i in range(self.led.numPixels()):
                self.led.setPixelColor(i, Color(255, 0, 0)) # Color is red
        elif value.upper() == "OFF":
            for i in range(self.led.numPixels()):
                self.led.setPixelColor(i, Color(0,0,0))
                
        self.led.show()

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
    led = LEDController(18, ACommand.Type.LIGHT_ON_OFF, "OFF")
    led.control_actuator("ON")
    time.sleep(1)
    led.control_actuator("OFF")
