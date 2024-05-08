import random
import time
from interfaces.sensors import ISensor, AReading

class MockDoorLockSensor(ISensor):
    
    def __init__(self, gpio: int, model: str, type: AReading.Type):
        self.gpio = gpio
        self.model = model
        self.type = type

    def read_sensor(self) -> AReading:
        locked = bool(random.randint(0, 1))
        return [
            AReading(AReading.Type.LOCK, AReading.Unit.NONE, locked)
        ]

if __name__ == "__main__":
    door_lock = MockDoorLockSensor(12, "MockDoorLockSensor", AReading.Type.LOCK)
    while True:
        locked = door_lock.read_sensor()
        print(locked)
        time.sleep(1)
