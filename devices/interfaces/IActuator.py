from abc import ABC, abstractmethod
from enum import Enum


class ACommand(ABC):
    """Abstract class for actuator command. Can be instantiated directly or inherited.
    Also defines all possible command types via enums.
    """

    class Type(str, Enum):
        """Enum defining types of actuators that can be targets for a command
        """
        FAN = 'fan'
        LIGHT_ON_OFF = 'light-on-off'
        LIGHT_PULSE = 'light-pulse'

    # Class properties that must be defined in implementation classes

    def __init__(self, target: Type, value: str) -> None:
        """Constructor for Command abstract class

        :param Type target: Type of command whih associated a command to a type of actuator.
        :param str value: Value of command to be passed to actuator.
        """
        self.target_type = target
        self.value: str = value

    def __repr__(self) -> str:
        return f'Command setting {self.target_type} to {self.value}'


class IActuator(ABC):

    # Class properties that must be set in constructor of implementation class
    _current_state: str
    type: ACommand.Type

    @abstractmethod
    def __init__(self, gpio: int, type: ACommand.Type, initial_state: str) -> None:
        """Constructor for Actuator class. Must define interface's class properties

        :param ACommand.Type type: Type of command the actuator can respond to.
        :param str initial_state: initializes 'current_state' property of a new actuator.
        If not passed, actuator implementation is responsible for setting a default value.
        """
        pass

    @abstractmethod
    def validate_command(self, command: ACommand) -> bool:
        """Validates that a command can be used with the specific actuator.

        :param ACommand command: the command to be validated.
        :return bool: True if command can be consumed by the actuator.
        """
        pass

    @abstractmethod
    def control_actuator(self, value: str) -> bool:
        """Sets the actuator to the value passed as argument.

        :param str value: Value used to set the new state of the actuator. Value is parsed inside concrete classes.
        :return bool: True if the state of the actuator changed, false otherwise.
        """
        pass