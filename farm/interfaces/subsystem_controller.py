from abc import ABC, abstractmethod
from interfaces.sensors import ISensor, AReading
from interfaces.actuators import IActuator, ACommand


from abc import ABC, abstractmethod


class SubsystemController(ABC):
    """Interface for a subsystem controller.

    This interface defines the methods that a subsystem controller should implement.
    """

    @abstractmethod
    def _initialize_sensors(self) -> list[ISensor]:
        """Initializes all sensors and returns them as a list. Intended to be used in class constructor.

        :return List[ISensor]: List of initialized sensors.
        """
        pass

    @abstractmethod
    def _initialize_actuators(self) -> list[IActuator]:
        """Initializes all actuators and returns them as a list. Intended to be used in class constructor.

        :return list[IActuator]: List of initialized actuators.
        """
        pass

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

    def get_actuator_states(self) -> list[dict]:
        actuators = []
        for actuator in self._actuators:
            actuators.append({"target": actuator.type.value, "value": actuator.state})
        return actuators
