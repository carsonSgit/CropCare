from grove import grovepi
from interfaces.actuators import IActuator, ACommand
from time import sleep


class VibrationController(IActuator):

    def __init__(self, pin: int, type: ACommand.Type, callback=None) -> None:
        self.pin = pin
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
        grovepi.pinMode(self.pin, "INPUT")
        while True:
            try:
                value = grovepi.digitalRead(self.pin)
                if value:
                    self._handle_vibration()
                sleep(2)
            except IOError:
                print("Error")


if __name__ == "__main__":
    def callback():
        print("Alert: Vibration detected")

    vibration_sensor = VibrationController(2, ACommand.Type.VIBRATION, callback)
    vibration_sensor.start_detection()
