from interfaces.sensors import ISensor, AReading
from time import sleep
from grove.grove_mini_pir_motion_sensor import GroveMiniPIRMotionSensor

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
        self.motion = GroveMiniPIRMotionSensor(gpio)
        self.detected = False
        self.motion.on_detect = self.motion_detected

    def motion_detected(self):
        self.detected = True

    def read_sensor(self) -> list[AReading]:
        """
        Reads the motion sensor and returns a list of motion readings.

        Returns:
            list[AReading]: A list of motion readings.

        """
        detected = self.detected
        self.detected = False
        return [
            AReading(AReading.Type.MOTION, AReading.Unit.NONE, detected),
        ]
    
if __name__ == "__main__":
    m = MotionSensor(22, "motion", AReading.Type.MOTION)

    while(True):
        print(m.read_sensor())
        sleep(1)