from interfaces.actuators import ACommand
from interfaces.sensors import AReading
from interfaces.subsystem_controller import SubsystemController
from subsystems.geolocation.geolocation_controller import GeolocationController
from subsystems.plant.plant_controller import PlantController
from subsystems.security.security_controller import SecurityController
from time import sleep


class DeviceController:
    """
    The DeviceController class is responsible for managing the subsystem controllers and coordinating the reading of sensors
    and control of actuators.

    Attributes:
        _controllers (list[SubsystemController]): A list of subsystem controllers.

    Methods:
        __init__(): Initializes the DeviceController object.
        _initialize_controllers(): Initializes the subsystem controllers.
        read_sensors(): Reads the sensors from all subsystem controllers.
        control_actuators(commands: list[ACommand]): Controls the actuators of all subsystem controllers.
    """

    def __init__(self) -> None:
        self._controllers: list[SubsystemController] = self._initialize_controllers()

    def _initialize_controllers(self) -> list[SubsystemController]:
        """
        Initializes the subsystem controllers.

        Returns:
            list[SubsystemController]: A list of initialized subsystem controllers.
        """
        return [
            SecurityController(),
            PlantController(),
            GeolocationController()
        ]
    
    def read_sensors(self) -> list[AReading]:
        """
        Reads the sensors from all subsystem controllers.

        Returns:
            list[AReading]: A list of sensor readings.
        """
        readings = []
        for controller in self._controllers:
            readings.extend(controller.read_sensors())
        return readings
    
    def control_actuators(self, commands: list[ACommand]) -> None:
        """
        Controls the actuators of all subsystem controllers.

        Args:
            commands (list[ACommand]): A list of commands to control the actuators.
        """
        for controller in self._controllers:
            controller.control_actuators(commands)

if __name__ == "__main__":
    device_manager = DeviceController()
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
