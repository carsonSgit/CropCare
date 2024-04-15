import random
from interfaces.actuators import IActuator, ACommand
from time import sleep


class MockVibrationController(IActuator):
    def __init__(self, type: ACommand.Type, callback=None) -> None:
        self.type = type
        self.callback = callback

    def control_actuator(self, value: str) -> bool:
        return True

    def validate_command(self, command: ACommand) -> bool:
        return command.target_type == self.type and command.value.upper() in ["ON", "OFF"]

    def set_callback(self, callback):
        self.callback = callback

    def _handle_vibration(self):
        if self.callback:
            self.callback()

    def start_detection(self):
        while True:
            try:
                if random.random() > 0.7:
                    self._handle_vibration()
                sleep(2)
            except KeyboardInterrupt:
                break


if __name__ == "__main__":
    def callback():
        print("Alert: Vibration detected")

    mock_vibration_sensor = MockVibrationController(ACommand.Type.VIBRATION, callback)
    mock_vibration_sensor.start_detection()
