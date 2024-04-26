from interfaces.sensors import ISensor, AReading
import seeed_python_reterminal.acceleration as rt_accel
import seeed_python_reterminal.core as rt
import math

class AccelSensor(ISensor):
    """
    Represents an accelerometer sensor.

    Args:
        gpio (int): The GPIO pin number.
        model (str): The model of the accelerometer sensor.
        type (AReading.Type): The type of the accelerometer reading.

    Attributes:
        gpio (int): The GPIO pin number.
        model (str): The model of the accelerometer sensor.
        type (AReading.Type): The type of the accelerometer reading.

    """

    def __init__(self, gpio: int, model: str, type: AReading.Type):
        self.gpio = gpio
        self.model = model
        self.type = type
        self.xyz = [None, None, None]
        self.device = rt.get_acceleration_device()

    def read_sensor(self) -> list[AReading]:
        """
        Reads the accelerometer sensor and returns a list of accelerometer readings.

        Returns:
            list[AReading]: A list of accelerometer readings.

        """
        for event in self.device.read_loop():
            accelEvent = rt_accel.AccelerationEvent(event)
            if accelEvent.name != None:
                if "X" in str(accelEvent.name).upper():
                    self.xyz[0] = accelEvent.value
                if "Y" in str(accelEvent.name).upper():
                    self.xyz[1] = accelEvent.value
                if "Z" in str(accelEvent.name).upper():
                    self.xyz[2] = accelEvent.value
            if None not in self.xyz:
                break

        pitch = 180 * math.atan2(self.xyz[1], math.sqrt(self.xyz[0]**2 + self.xyz[2]**2)) / math.pi
        roll = 180 * math.atan2(self.xyz[0], math.sqrt(self.xyz[1]**2 + self.xyz[2]**2)) / math.pi

        self.xyz = [None, None, None]
        
        return [
            AReading(AReading.Type.ROLL, AReading.Unit.DEGREE, roll),
            AReading(AReading.Type.PITCH, AReading.Unit.DEGREE, pitch),
        ]

if __name__ == "__main__":
    sensor = AccelSensor(0, "accel", AReading.Type.PITCH)
    while True:
        readings = sensor.read_sensor()

        print("roll: " + str(readings[0]))
        print("pitch: " + str(readings[1]))
