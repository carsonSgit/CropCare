from azure.iot.device import MethodResponse, MethodRequest
from typing import List
from interfaces.actuators import ACommand
import json


class StatusCode:
    SUCCESS = 200
    FAILED = 400


class RequestProcessor:

    def __init__(
        self, control_actuators_callback: callable, get_states_callback: callable
    ):
        self.control_actuators = control_actuators_callback
        self.get_states = get_states_callback

    def __call__(self, method_request: MethodRequest) -> MethodResponse:
        return self.process_request(method_request)

    def process_request(self, method_request: MethodRequest) -> MethodResponse:
        method = getattr(self, method_request.name, self.method_not_found)
        return method(method_request)

    def is_online(self, method_request: MethodRequest) -> MethodResponse:
        response_payload = {"response": "Device is online"}
        return MethodResponse.create_from_method_request(
            method_request, StatusCode.SUCCESS, response_payload
        )

    def method_not_found(self, method_request: MethodRequest) -> MethodRequest:
        response_payload = {"details": "method name unknown"}
        return MethodResponse.create_from_method_request(
            method_request, StatusCode.FAILED, response_payload
        )

    def control_actuator(self, method_request: MethodRequest) -> MethodRequest:
        try:
            command: dict = method_request.payload
            self.control_actuators(
                [ACommand(command.get("target"), command.get("value"))]
            )
            response_payload = {
                "response": f"{command.get('target')} was set to {command.get('value')}"
            }
            status = StatusCode.SUCCESS
        except Exception as e:
            print(e)
            response_payload = {"details": e}
            status = StatusCode.SUCCESS

        return MethodResponse.create_from_method_request(
            method_request, status, response_payload
        )

    def get_actuator_states(self, method_request: MethodRequest) -> MethodRequest:
        try:
            states: list = self.get_states()
            payload = json.dumps(states)
            status = StatusCode.SUCCESS

        except Exception as e:
            print(e)
            status = StatusCode.FAILED
            payload = {"details": e}

        return MethodResponse.create_from_method_request(
            method_request, status, payload
        )

    def get_single_actuator_state(self, method_request: MethodRequest) -> MethodRequest:
        try:
            states: list[dict] = self.get_states()
            target: str = method_request.payload.get("target")
            status = StatusCode.FAILED
            payload = {"details": f"Actuator '{target}' not found"}

            for state in states:
                if state.get("target") == target:
                    status = StatusCode.SUCCESS
                    payload = state
                    break

        except Exception as e:
            status = StatusCode.FAILED
            payload = {"details": e}

        return MethodResponse.create_from_method_request(
            method_request, status, payload
        )
