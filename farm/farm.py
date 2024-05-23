import asyncio
from azure.iot.device import MethodResponse, MethodRequest, Message
from azure.iot.device.aio import IoTHubDeviceClient
from azure.iot.device.custom_typing import TwinPatch
import os
import json
from util.farm_req_processor import RequestProcessor
from interfaces.actuators import ACommand
from interfaces.sensors import AReading
from interfaces.subsystem_controller import SubsystemController
from subsystems.geolocation.geolocation_controller import GeolocationController
from subsystems.plant.plant_controller import PlantController
from subsystems.security.security_controller import SecurityController
import dotenv

dotenv.load_dotenv(override=True)

READING_RATE = 5
CONNECTION_STRING = os.environ.get("DEVICE_CONNECTION_STRING")


class Farm:
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
        self.client = IoTHubDeviceClient.create_from_connection_string(
            CONNECTION_STRING
        )
        self._controllers: list[SubsystemController] = self._initialize_controllers()
        self.client.on_method_request_received = self.handle_method_request
        self.client.on_twin_desired_properties_patch_received = self.handle_twin_patch
        self.telemetry_interval = READING_RATE
        self.request_processor = RequestProcessor(
            self.control_actuators, self.get_actuator_states
        )

    async def handle_method_request(self, method_request: MethodRequest) -> None:
        """
        Handles direct method requests from IoT Hub by passing it to the request processor.

        Args:
            method_request (MethodRequest): The method request received.
        """
        method_response = self.request_processor(method_request)
        await self.client.send_method_response(method_response)

    def handle_twin_patch(self, twin_patch: TwinPatch) -> None:
        """
        Handle updates to the device twin's desired properties.

        Args:
            twin_patch (TwinPatch): Received twin path to handle.
        """
        if "telemetryInterval" in twin_patch:
            new_interval = twin_patch["telemetryInterval"]
            self.telemetry_interval = new_interval

    def _initialize_controllers(self) -> list[SubsystemController]:
        """
        Initializes the subsystem controllers.

        Returns:
            list[SubsystemController]: A list of initialized subsystem controllers.
        """
        return [SecurityController(), PlantController(), GeolocationController()]

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

    def get_actuator_states(self) -> list[dict]:
        states = []
        for controller in self._controllers:
            states.extend(controller.get_actuator_states())
        return states

    def control_actuators(self, commands: list[ACommand]) -> None:
        """
        Controls the actuators of all subsystem controllers.

        Args:
            commands (list[ACommand]): A list of commands to control the actuators.
        """
        for controller in self._controllers:
            controller.control_actuators(commands)

    async def send_readings(self) -> None:
        """
        Sends the sensor readings to the client.

        This method reads the sensor values and sends them to the client using the
        `send_message` method of the client object.

        Returns:
            None
        """
        readings = self.read_sensors()

        for reading in readings:
            message = Message(reading.export_json())
            await self.client.send_message(message)

    async def __call__(self) -> None:
        """
        Connects to the client and sends readings at regular intervals.
        """
        await self.client.connect()
        while True:
            await self.send_readings()
            await asyncio.sleep(self.telemetry_interval)


if __name__ == "__main__":
    farm_manager = Farm()
    asyncio.run(farm_manager())
