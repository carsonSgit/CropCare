from interfaces.sensors import ISensor, AReading
from gpiozero import MotionSensor
from time import sleep


class MotionSensor(ISensor):
    """
    Represents a motion sensor device.

    Args:
        gpio (int): The GPIO pin number of the motion sensor.
        model (str): The model of the motion sensor.
        type (AReading.Type): The type of reading produced by the motion sensor.

    Attributes:
        gpio (int): The GPIO pin number of the motion sensor.
        model (str): The model of the motion sensor.
        type (AReading.Type): The type of reading produced by the motion sensor.
        motion (MotionSensor): The motion sensor instance.

    """

    def __init__(self, gpio: int, model: str, type: AReading.Type):
        """
        Initializes a Motion object.

        Args:
            gpio (int): The GPIO pin number for the motion sensor.
            model (str): The model of the motion sensor.
            type (AReading.Type): The type of reading for the motion sensor.

        Returns:
            None
        """
        self.gpio = gpio
        self.model = model
        self.type = type
        self.motion = MotionSensor(gpio)

    def read_sensor(self) -> list[AReading]:
        """
        Reads the motion sensor and returns a list of motion readings.

        Returns:
            list[AReading]: A list of motion readings.

        """
        return [
            AReading(AReading.Type.MOTION, AReading.Unit.NONE, self.motion.motion_detected),
        ]
    
if __name__ == "__main__":
    m = MotionSensor(12, "motion", AReading.Type.MOTION)

    while(True):
        print(m.read_sensor())
        sleep(1)