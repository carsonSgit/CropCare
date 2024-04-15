from interfaces.sensors import ISensor, AReading
from time import sleep
from interfaces.actuators import IActuator, ACommand
from dotenv import load_dotenv
import os

load_dotenv(override=True)
env = os.environ["ENVIRONMENT_MODE"]

if env == "prod":
    from device.temp import TempController
    from device.fan import FanController
    from device.led import LEDController
    from device.buzzer import BuzzerController
else:
    from mock.mock_temp import (
        MockTempController as TempController,
    )
    from mock.mock_fan import MockFanController as FanController
    from mock.mock_led import MockLEDController as LEDController
    from mock.mock_buzzer import MockBuzzerController as BuzzerController


class DeviceController:

    def __init__(self) -> None:
        self._sensors: list[ISensor] = self._initialize_sensors()
        self._actuators: list[IActuator] = self._initialize_actuators()

    def _initialize_sensors(self) -> list[ISensor]:
        """Initializes all sensors and returns them as a list. Intended to be used in class constructor.

        :return List[ISensor]: List of initialized sensors.
        """

        return [
            # Instantiate each sensor inside this list, separate items by comma.
            TempController(4, "AHT20", AReading.Type.TEMPERATURE)
        ]

    def _initialize_actuators(self) -> list[IActuator]:
        """Initializes all actuators and returns them as a list. Intended to be used in class constructor

        :return list[IActuator]: List of initialized actuators.
        """

        return [
            # Instantiate each actuator inside this list, separate items by comma.
            FanController(12, ACommand.Type.FAN, "OFF"),
            LEDController(16, ACommand.Type.LIGHT_ON_OFF, "OFF"),
            BuzzerController(26, ACommand.Type.BUZZER, "OFF")
        ]

    def read_sensors(self) -> list[AReading]:
        """Reads data from all initialized sensors.

        :return list[AReading]: a list containing all readings collected from sensors.
        """
        readings: list[AReading] = []
        for sensor in self._sensors:
            readings.extend(sensor.read_sensor())

        return readings

    def control_actuators(self, commands: list[ACommand]) -> None:
        """Controls actuators according to a list of commands. Each command is applied to it's respective actuator.

        :param list[ACommand] commands: List of commands to be dispatched to corresponding actuators.
        """
        for command in commands:
            for actuator in self._actuators:
                if actuator.validate_command(command):
                    actuator.control_actuator(command.value)
                    break


if __name__ == "__main__":
    """
    This script is intented to be used as a module, however, code below can be used for testing.
    """
    device_manager = DeviceController()
    TEST_SLEEP_TIME = 2

    while True:
        commands = [
            ACommand(ACommand.Type.FAN, "ON"),
            ACommand(ACommand.Type.LIGHT_ON_OFF, "ON"),
            ACommand(ACommand.Type.BUZZER, "ON"),
        ]
        device_manager.control_actuators(commands)
        print(device_manager.read_sensors())
        sleep(TEST_SLEEP_TIME)

        commands = [
            ACommand(ACommand.Type.FAN, "OFF"),
            ACommand(ACommand.Type.LIGHT_ON_OFF, "OFF"),
            ACommand(ACommand.Type.BUZZER, "OFF"),
        ]
        device_manager.control_actuators(commands)
        print(device_manager.read_sensors())
        sleep(TEST_SLEEP_TIME)