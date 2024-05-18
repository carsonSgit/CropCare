from interfaces.sensors import ISensor, AReading
import seeed_python_reterminal.acceleration as rt_accel
import seeed_python_reterminal.core as rt
import math
import collections
import time


class VibrationSensor(ISensor):
    """
    Represents an accelerometer sensor for detecting vibrations.

    Args:
        gpio (int): The GPIO pin number.
        model (str): The model of the accelerometer sensor.
        type (AReading.Type): The type of the accelerometer reading.

    Attributes:
        gpio (int): The GPIO pin number.
        model (str): The model of the accelerometer sensor.
        type (AReading.Type): The type of the accelerometer reading.
        threshold (float): The acceleration change threshold to detect vibration.
        window_size (int): The number of recent acceleration readings to store.
        accel_readings (collections.deque): A deque storing recent acceleration readings.
        baseline (float): The baseline magnitude to compare against for vibration detection.
    """

    def __init__(
        self,
        gpio: int,
        model: str,
        type: AReading.Type,
        threshold: float = 10.0,
        window_size: int = 10,
    ):
        self.gpio = gpio
        self.model = model
        self.type = type
        self.xyz = [None, None, None]
        self.device = rt.get_acceleration_device()
        self.threshold = threshold
        self.window_size = window_size
        self.accel_readings = collections.deque(maxlen=window_size)
        self.baseline = self.calibrate_baseline()
        self.vibration_counter = 0
        self.vibration_sustained_threshold = 3  # Number of consecutive readings indicating vibration to confirm vibration

    def calibrate_baseline(self) -> float:
        """
        Calibrates the baseline magnitude for the sensor.

        Returns:
            float: The baseline magnitude.
        """
        baseline_readings = []
        for _ in range(self.window_size):
            for event in self.device.read_loop():
                accelEvent = rt_accel.AccelerationEvent(event)
                if accelEvent.name is not None:
                    if "X" in str(accelEvent.name).upper():
                        self.xyz[0] = accelEvent.value
                    if "Y" in str(accelEvent.name).upper():
                        self.xyz[1] = accelEvent.value
                    if "Z" in str(accelEvent.name).upper():
                        self.xyz[2] = accelEvent.value
                if None not in self.xyz:
                    break
            magnitude = math.sqrt(
                self.xyz[0] ** 2 + self.xyz[1] ** 2 + self.xyz[2] ** 2
            )
            baseline_readings.append(magnitude)
            self.xyz = [None, None, None]
        return sum(baseline_readings) / len(baseline_readings)

    def read_sensor(self) -> list[AReading]:
        """
        Reads the accelerometer sensor and returns a list of accelerometer readings.

        Returns:
            list[AReading]: A list of accelerometer readings.
        """
        for event in self.device.read_loop():
            accelEvent = rt_accel.AccelerationEvent(event)
            if accelEvent.name is not None:
                if "X" in str(accelEvent.name).upper():
                    self.xyz[0] = accelEvent.value
                if "Y" in str(accelEvent.name).upper():
                    self.xyz[1] = accelEvent.value
                if "Z" in str(accelEvent.name).upper():
                    self.xyz[2] = accelEvent.value
            if None not in self.xyz:
                break

        magnitude = math.sqrt(self.xyz[0] ** 2 + self.xyz[1] ** 2 + self.xyz[2] ** 2)

        self.accel_readings.append(magnitude)

        vibration_detected = False
        if len(self.accel_readings) == self.window_size:
            avg_magnitude = sum(self.accel_readings) / self.window_size
            if abs(avg_magnitude - self.baseline) > self.threshold:
                self.vibration_counter += 1
            else:
                self.vibration_counter = 0

            if self.vibration_counter >= self.vibration_sustained_threshold:
                vibration_detected = True
            else:
                vibration_detected = False

        self.xyz = [None, None, None]

        return [
            AReading(AReading.Type.VIBRATION, AReading.Unit.NONE, vibration_detected),
        ]


if __name__ == "__main__":
    sensor = VibrationSensor(0, "accel", AReading.Type.PITCH)
    while True:
        readings = sensor.read_sensor()
        print("vibration detected: " + str(readings[0]))
        time.sleep(0.1)
