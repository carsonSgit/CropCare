from interfaces.subsystem_controller import SubsystemController
from interfaces.sensors import ISensor, AReading
from time import sleep
from interfaces.actuators import IActuator, ACommand
from dotenv import load_dotenv
import os

load_dotenv(override=True)
env = os.environ["ENVIRONMENT_MODE"]

if env == "prod":
    from subsystems.plant.soilmoisture import SoilMoistureSensor
    from subsystems.plant.temp import TempController
    from subsystems.plant.waterlevel import WaterLevelSensor
    from subsystems.plant.fan import FanController
    from subsystems.plant.led import LEDController
else:
    from mock.subsystems.plant.mock_soilmoisture import MockSoilMoistureSensor as SoilMoistureSensor
    from mock.subsystems.plant.mock_temp import MockTempController as TempController
    from mock.subsystems.plant.mock_waterlevel import MockWaterLevelSensor as WaterLevelSensor
    from mock.subsystems.plant.mock_fan import MockFanController as FanController
    from mock.subsystems.plant.mock_led import MockLEDController as LEDController

class PlantController(SubsystemController):
    """
    The PlantController class is responsible for controlling the plant subsystem.
    It initializes the sensors and actuators required for monitoring and controlling the plant environment.
    """

    def __init__(self) -> None:
        self._sensors: list[ISensor] = self._initialize_sensors()
        self._actuators: list[IActuator] = self._initialize_actuators()

    def _initialize_sensors(self) -> list[ISensor]:
        return [
            SoilMoistureSensor(0, "Grove Moisuture Reader", AReading.Type.MOISTURE),
            TempController(4, "AHT20", AReading.Type.TEMPERATURE),
            WaterLevelSensor(5, "Grove Waterlevel Reader", AReading.Type.WATERLEVEL)
        ]

    def _initialize_actuators(self) -> list[IActuator]:
        return [
            FanController(12, ACommand.Type.FAN, "OFF"),
            LEDController(18, ACommand.Type.LIGHT_ON_OFF, 'OFF')
        ]


if __name__ == "__main__":
    device_manager = PlantController()
    TEST_SLEEP_TIME = 2

    while True:
        commands = [
            ACommand(ACommand.Type.FAN, "ON"),
            ACommand(ACommand.Type.LIGHT_ON_OFF, "ON"),
        ]
        device_manager.control_actuators(commands)
        sleep(TEST_SLEEP_TIME)
        commands = [
            ACommand(ACommand.Type.FAN, "OFF"),
            ACommand(ACommand.Type.LIGHT_ON_OFF, "OFF"),
        ]
        device_manager.control_actuators(commands)
        print(device_manager.read_sensors())
        sleep(TEST_SLEEP_TIME)