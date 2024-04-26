from interfaces.subsystem_controller import SubsystemController
from interfaces.sensors import ISensor, AReading
from time import sleep
from interfaces.actuators import IActuator, ACommand
from dotenv import load_dotenv
import os

load_dotenv(override=True)
env = os.environ["ENVIRONMENT_MODE"]

if env == "prod":
    from subsystems.geolocation.accel import AccelSensor
    from subsystems.geolocation.gps import GPS
    from subsystems.geolocation.buzzer import BuzzerController
else:
    from mock.subsystems.geolocation.mock_accel import MockAccelSensor as AccelSensor
    from mock.subsystems.geolocation.mock_gps import MockGPSSensor as GPS
    from mock.subsystems.geolocation.mock_buzzer import MockBuzzerController as BuzzerController


class GeolocationController(SubsystemController):
    """
    The GeolocationController class is responsible for managing the geolocation subsystem.

    Attributes:
        _sensors (list[ISensor]): A list of sensors used in the geolocation subsystem.
        _actuators (list[IActuator]): A list of actuators used in the geolocation subsystem.
    """

    def __init__(self) -> None:
        self._sensors: list[ISensor] = self._initialize_sensors()
        self._actuators: list[IActuator] = self._initialize_actuators()

    def _initialize_sensors(self) -> list[ISensor]:
        return [
            AccelSensor(-1, "LIS3DHTR", AReading.Type.ROLL),
            GPS('/dev/ttyS0', 9600, AReading.Type.GPS)
        ]

    def _initialize_actuators(self) -> list[IActuator]:
        return [
            BuzzerController(-1, ACommand.Type.BUZZER, "OFF"),
        ]


if __name__ == "__main__":
    device_manager = GeolocationController()
    TEST_SLEEP_TIME = 2

    while True:
        commands = [
            ACommand(ACommand.Type.BUZZER, "ON"),
        ]
        device_manager.control_actuators(commands)

        commands = [
            ACommand(ACommand.Type.BUZZER, "OFF"),
        ]
        device_manager.control_actuators(commands)
        print(device_manager.read_sensors())
        sleep(TEST_SLEEP_TIME)