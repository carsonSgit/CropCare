from azure.iot.device import IoTHubDeviceClient, MethodResponse, MethodRequest
import os
import json
from interfaces.actuators import ACommand
from interfaces.sensors import AReading
from interfaces.subsystem_controller import SubsystemController
from subsystems.geolocation.geolocation_controller import GeolocationController
from subsystems.plant.plant_controller import PlantController
from subsystems.security.security_controller import SecurityController
from time import sleep
import dotenv
dotenv.load_dotenv(override=True)

READING_RATE = 5
CONNECTION_STRING = os.environ.get("DEVICE_CONNTECTION_STRING")

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
        self.client = IoTHubDeviceClient.create_from_connection_string(CONNECTION_STRING)
        self._controllers: list[SubsystemController] = self._initialize_controllers()
        self.client.on_method_request_received = self.handle_method_request

    def handle_method_request(self, method_request: MethodRequest):
        """Handles direct method requests from IoT Hub"""
        if method_request.name == "is_online":
            response_payload = {"response": "Device is online"}
            response_status = 200
        else:
            response_payload = {"details": "method name unknown"}
            response_status = 400

        method_response = MethodResponse.create_from_method_request(
            method_request, response_status, response_payload
        )
        self.client.send_method_response(method_response)

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

    def run(self):
        self.client.connect()
        while True:
            
            readings = self.read_sensors()
            payload_as_dict = {}

            for reading in readings:
                payload_as_dict[reading.reading_type] = reading.value

            payload = json.dumps(payload_as_dict)
            print(payload)
            self.client.send_message(payload)

            sleep(READING_RATE)

if __name__ == "__main__":
    device_manager = DeviceController()
    device_manager.run()
