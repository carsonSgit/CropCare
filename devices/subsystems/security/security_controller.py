from interfaces.subsystem_controller import SubsystemController
from interfaces.sensors import ISensor, AReading
from time import sleep
from interfaces.actuators import IActuator, ACommand
from dotenv import load_dotenv
import os

load_dotenv(override=True)
env = os.environ["ENVIRONMENT_MODE"]

if env == "prod":
    from subsystems.security.loudness import LoudnessSensor
    from subsystems.security.luminosity import LuminositySensor
    from subsystems.security.motion import MotionSensor
    from subsystems.security.servo import ServoController
else:
    from mock.subsystems.security.mock_loudness import MockLoudnessSensor as LoudnessSensor
    from mock.subsystems.security.mock_luminosity import MockLuminositySensor as LuminositySensor
    from mock.subsystems.security.mock_motion import MockMotionSensor as MotionSensor
    from mock.subsystems.security.mock_servo import MockServoController as ServoController

class SecurityController(SubsystemController):
    """
    The SecurityController class represents the controller for the security subsystem.

    It initializes and manages the sensors and actuators used for security monitoring and control.
    """

    def __init__(self) -> None:
        self._sensors: list[ISensor] = self._initialize_sensors()
        self._actuators: list[IActuator] = self._initialize_actuators()

    def _initialize_sensors(self) -> list[ISensor]:
        return [
            LoudnessSensor(0, "LoudnessSensor", AReading.Type.LOUDNESS),
            LuminositySensor(-1, "light", AReading.Type.LUMINOSITY),
            MotionSensor(12, "motion", AReading.Type.MOTION),
        ]

    def _initialize_actuators(self) -> list[IActuator]:
        return [
            ServoController(16, ACommand.Type.SERVO, "-1")
        ]


if __name__ == "__main__":
    device_manager = SecurityController()
    TEST_SLEEP_TIME = 2

    while True:
        commands = [
            ACommand(ACommand.Type.SERVO, "1"),
        ]
        device_manager.control_actuators(commands)
        sleep(TEST_SLEEP_TIME)
        commands = [
            ACommand(ACommand.Type.SERVO, "-1"),
        ]
        device_manager.control_actuators(commands)
        print(device_manager.read_sensors())
        sleep(TEST_SLEEP_TIME)