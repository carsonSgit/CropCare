![image](https://github.com/carsonSgit/CropCare/assets/92652800/2ce6cfa8-36e8-4f52-916c-8e7c0db2ccbf)
   	 
<br/>

## 🌱 **Project Description**
CropCare's goal is to combat the growing impact urbanization has on the safety of the farming & agriculture world. Our application provides users (farm Technicians or Owners) with a centralized control & monitoring center for everything that is going on in their IoT container farms. The mobile application sends and receives data through Microsoft Azure which acts as an intermediary between our mobile application and the project back-end. The project back-end is developed in Python and hosted/ran off of a Rasperry Pi reTerminal that is repeatedly getting readings and states from the hardware it is connected with, ultimately sending it off to Microsoft Azure to then be received on the Mobile Application interface.

<br/>

## 🔧 **Contributions**

| Developer                  | Contribution                      |
|-----------------------|-----------------------------------|
| **[Cristiano Fazi](https://github.com/Cristiano-Fazi)**        | *Full-stack developer & team lead*  |
| **[Kevin Baggott](https://github.com/kbaggott1)**         | *Back-end & hardware developer*     |
| **[Carson Spriggs-Audet](https://github.com/carsonSgit)**  | *Full-stack developer & designer*   |

> [!IMPORTANT]
> It is important to know that there were no distinct set roles, the contributions above are what each person primarily focused on, but each team member evenly contributed in various ways.

<br/>

## 🛠️ Relevant Technologies

In relation to the courses this project was made for, below are the technologies used;

**Application Development 3:**
  - Development in .NET Maui
  - Visual Studio 2022 Community Edition

**Connected Objects:**
  - Python
  - Azure IoT Tools
  - Raspberry Pi
  - Visual Studio Code

**External:**
  - [TinkerCAD](https://www.tinkercad.com/) *for creating the model*
  - Google's [model-viewer](https://github.com/google/model-viewer) *for displaying the exported CAD file*
  - [GitHub Pages](https://pages.github.com/) *for hosting the 3D Model*

<br/>

## 🌐 3D Model

<a href="https://carsonsgit.github.io/cropcare-3d/">
    <img src="https://github.com/carsonSgit/CropCare/assets/92652800/94c7410c-1eea-4078-bdac-dc9dc733ff1c" alt="3D Model" width="350"/>
</a>

> [!TIP]
> If you want to interact with the 3D model, please click the image above or you can follow [this link](https://carsonsgit.github.io/cropcare-3d/).

<br/>

## 🔗 Design Document

If you are interested in the design of our app and code structure (i.e. our "blueprint" before we began developing), please visit [this link](https://docs.google.com/document/d/1CgeMB0Ia7MkWsxPN-nKMrvUxbPpmW7MrcGwJhg-tKfA/edit?usp=sharing), you will be met with a Google Doc with our detailed strategy.

<br/>

## 📄 Other documents & files

If you are interested in viewing more details/more expansive versions of the information presented in this README (3D models, UML diagrams, wireframes), navigate to the **"./Documents"** path.
![image](https://github.com/carsonSgit/CropCare/assets/92652800/59aabf13-2b9b-46c5-af77-0c196a496c64)

<br/>

## App Layout

Below is an image displaying the functionality of each page within our mobile app (in light mode).
![CropCare App Layout](https://github.com/carsonSgit/CropCare/assets/92652800/f87b2c54-088b-4cd1-b7be-5aca8196e6dd)

### ⚙️ **App Setup**
You need the following technology
	- .NET MAUI & an Android Emulator
	- A Raspberry Pi & a base hat
 	- Various sensors & actuators (all noted below)

For .NET MAUI setup, you need to modify the appsettings.json to have the correct keys. Your appsettings.json should go in the project top level project directory. It should look like this:
```
{
  "Settings": {
    "FirebaseAuthorizedDomain": "my-firebase-app-d60e2.firebaseapp.com",
    "FireBaseApiKey": "awdawdFWADSf32134dadwawda",
    "FireBaseDatabaseURL": "https://my-firebase-app-d60e2-default-rtdb.firebaseio.com/",
    "GoogleMapsAPIKey": "gdtrhrdffeff3fsdffasefsegjntsbgfawddwa",
    "EventHubConnectionString": "Endpoint=sb://iothub-ns-my-app-59465961-6d1bd0200e.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=0es2007K2ch9jfd6n9rDBX1kEIX8HkAc9AIoTHoehx0=;EntityPath=my-app",
    "EventHubName": "my-app",
    "IotHubConnectionString": "HostName=my-app.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=0es2007K2ch9jfd6n9rDBX1kEIX8HkAc9AIoTHoehx0="
  }
}
```
In order to get a google maps api key, follow the intstructions here: [Use API Keys](https://developers.google.com/maps/documentation/javascript/get-api-key)

For the azure setup, you must create an iothub resource. To get the EventHubConnectionString and EventHubName you must go to the Build-in endpoints tab and you will see them there.
As for the iothubconnection string you must go to the Shared access policies tab, click on iothubowner and copy the primary connection string from there.
There firebase API keys are all obtained from the firebase console.

Once azure is set up, you must create a device, click on it and copy its primary key or connection string and put it in a .env in farm/ folder in this repo. You can use .env.example as an example of how your .env should look. 
In order to properly get readings from iot hub from within the app, when you create a farm you must enter the device_id that matches the correct device in Azure iot hub: 

![image](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-cropcare/assets/93663166/be141589-87fb-4e08-afb0-5a551934214c)
![image](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-cropcare/assets/93663166/d9da34f5-9a83-414d-9a13-23b22336dd71)

# 🎮 Controlling Actuators

## 🌱 Plant Subsystem Device Documentation

Follow the port guide below to sucessfully utilize the farm setup. 

| Sensor/Actuator       		| Port on Grove Base Hat | Port Type   | Unit                 |
|---------------------------|------------------------|-------------|----------------------|
| Soil Moisture Sensor  		| 0                      | PIN         | Ω                    |
| Temperature/Humidity Sensor 	| 26                  | PIN         | Temp: °C, Humi: % HR |
| Water Level Sensor    		| 5                      | PIN         | water level          |
| Fan                   		| 12                     | PIN         | N/A                  |
| LED                   		| 18                     | PIN         | N/A                  |

## 🔒 Security Subsystem Device Documentation

Follow the port guide below to sucessfully utilize the farm setup.

| Sensor/Actuator       | Port on Grove Base Hat | Port Type   | Unit     |
|-----------------------|------------------------|-------------|----------|
| Loudness Sensor       | 2                      | PIN         | unitless |
| Luminosity Sensor     | N/A                    | BUS         | nm       |
| Motion Sensor         | 22                     | PIN         | N/A      |
| Vibration Sensor      | N/A                     | PIN         | N/A      |
| Servo                 | 16                     | PIN         | N/A      |
| Door Sensor           | 24                     | PIN         | N/A      |

## 🌍 Geolocation Subsystem Device Documentation

Follow the port guide below to sucessfully utilize the farm setup.

| Sensor/Actuator | Port on Grove Base Hat | Port Type | Unit |
|-----------------|------------------------|-----------|------|
| Accelerometer   | N/A                    | BUS       | °    |
| Buzzer          | N/A                    | BUS       | N/A  |
| GPS             | /dev/ttyS0             | UART      | N/A  |

## 🛠️ Controlling the Actuators from the Cloud:
Direct Methods are used to control each actuator. We felt that it made the most sense since we control them
with a method in our code to just invoke the same method with the parameters specified in the payload.

### 🌀 Fan
The target field must be set to fan and the value can be either on or off. 
`az iot hub invoke-device-method --hub-name cropcare --device-id {device_id} --method-name control_actuator --method-payload '{"target":"fan", "value": "on"}'`

### 🔊 Buzzer
The target field must be set to buzzer and the value can be either on or off.
`az iot hub invoke-device-method --hub-name cropcare --device-id {device_id} --method-name control_actuator --method-payload '{"target":"buzzer", "value": "on"}'`

### 🎚️ Servo
The target field must be set to servo and the value can be a float from -1 to 1 inclusively.
`az iot hub invoke-device-method --hub-name cropcare --device-id {device_id} --method-name control_actuator --method-payload '{"target":"servo", "value": "1"}'`

### 💡 LED
The target field must be set to led and the value can be either on or off.
`az iot hub invoke-device-method --hub-name cropcare --device-id {device_id} --method-name control_actuator --method-payload '{"target":"led", "value": "on"}'`

## 📡 Additional Communication from the Cloud
We can also get the state of any actuator we want without changing them by using the following command:
`az iot hub invoke-device-method --hub-name cropcare --device-id {device_id} --method-name get_single_actuator_state --method-payload '{"target":"fan"}'`

This will return a message resembling:
`{
  "payload": {
    "target": "fan",
    "value": "ON"
  },
  "status": 200
}`
To set telemetryInterval to 5 seconds:
`az iot hub device-twin update -n cropcare -d {device_id} --desired '{"telemetryInterval": 5}'`

## ⬆️ D2C Messages
Longitude and Latitude to determine GPS location: 
- `az iot device send-d2c-message -n {iothub_name} -d {device_id} --data "{'reading_type': 'LONGITUDE', 'value': 152.408976, 'unit': '°'}"`
- `az iot device send-d2c-message -n {iothub_name} -d {device_id} --data "{'reading_type': 'LATITUDE', 'value': 152.408976, 'unit': '°'}"`

Temperature of the container:
- `az iot device send-d2c-message -n {iothub_name} -d {device_id} --data "{'reading_type': 'TEMPERATURE', 'value': 40.34324, 'unit': '°C'}"`

Humidity inside the container:
- `az iot device send-d2c-message -n {iothub_name} -d {device_id} --data "{'reading_type': 'HUMIDITY', 'value': 20.231, 'unit': '% HR'}"`

Luminosity for detecting light levels:
- `az iot device send-d2c-message -n {iothub_name} -d {device_id} --data "{'reading_type': 'LUMINOSITY', 'value': 433, 'unit': 'lux'}"`

Loudness inside the container:
- `az iot device send-d2c-message -n {iothub_name} -d {device_id} --data "{'reading_type': 'LOUDNESS', 'value': 200, 'unit': 'Db'}"`

Moisture of the soil:
- `az iot device send-d2c-message -n {iothub_name} -d {device_id} --data "{'reading_type': 'MOISTURE', 'value': 232.34311, 'unit': 'Ω'}"`

Waterlevel:
- `az iot device send-d2c-message -n {iothub_name} -d {device_id} --data "{'reading_type': 'WATERLEVEL', 'value': 221, 'unit': 'Ω'}"`

Motion inside the container:
- `az iot device send-d2c-message -n {iothub_name} -d {device_id} --data "{'reading_type': 'MOTION', 'value': True, 'unit': ''}"`

Vibration in the container:
- `az iot device send-d2c-message -n {iothub_name} -d {device_id} --data "{'reading_type': 'VIBRATION', 'value': False, 'unit': ''}"`

Magnet for detecting if the door is closed:
- `az iot device send-d2c-message -n {iothub_name} -d {device_id} --data "{'reading_type': 'MAGNET', 'value': True, 'unit': ''}"`

Pitch of the container:
- `az iot device send-d2c-message -n {iothub_name} -d {device_id} --data "{'reading_type': 'PITCH', 'value': 12.322, 'unit': '°'}"`

Roll of the container:
- `az iot device send-d2c-message -n {iothub_name} -d {device_id} --data "{'reading_type': 'ROLL', 'value': 56.7445, 'unit': '°'}"`


### 🛠️ **App Overview**

#### Hardware Features <a name="hardware-features"/>
- Monitoring
  - Measure relative water levels
  - Measure soil moisture levels
  - Read fan state (on/off)
  - Read light state (on/off)
  - Collect container GPS Location
  - Read container pitch and roll angles
  - Read vibration levels
  - Read buzzer state (On/Off)
  - Read noise levels
  - Read luminosity levels
  - Read motion sensor state (motion detected)
  - Read door-lock state (servo open/closed)
  - Read door state

- Remote Controls
  - Control buzzer state (On/Off)
  - Control door-lock state (servo open/closed)
  - Control fan state (on/off)
  - Control light state (on/off)

#### ☁️ Cloud Computing Features <a name="cloud-computing-features"/>
- Azure IoT Setup
  - Able to create containers
  - Create azure keyvault to store connection strings

- IoT Access
  - Collect Telemetry data from Azure IoT hub
  - Allow for D2C commands to alter computer unit (Raspberry Pi reTerminal)'s state

### 📱 Mobile App Features <a name="mobile-app-features"/>
- User Authentication
  - Build a user login/signup page
  - Create a user login system
  - Create a user sign-up system (Considering the two account types)
  - Implement a logout button
  - Ensure app differentiates between account types

- Supporting Infrastructure
  - Create a flyout menu
  - Create a logo for the application and company
  - Access environment variables

- Overview
  - Owners are able to instantiate new farms
  - Owners & Technicians are able to enter farms assosciated with their user ID
 
- Dashboard
  - Users are able to see 3 general health statuses (Plant, Security, GPS)
  - Owners have the following cards; Plant, Security, GPS, Modify Technicians, Settings
  - Technicians have the following cards; Plant & Security

- Plant Monitoring Page
  - Display all the data relating to the plants, such as moisture, and water levels through an interactive page
  - Display health statuses according to current data

- Security Page
  - Display container state information
  - Allow users to control systems of the container farm, such as the door

- GPS Monitoring Page
  - Fleet manager should see a map on their farm page with the actual farm location
 
- Modify Technicians Page
  - The owner of a farm is able to modify the technicians who are assigned to a farm
 
- Farm Settings Page
  - Owners can; change farm name, change twin patch interval, and the icon of their farm

- Settings Page
  - Design Settings Page
  - Allow user to change account details
  - Allow users to change colour theme

### 🎨 App Wireframes <a name="app-prototype"/>
<!-- ![image](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-cropcare/assets/98350514/13b4257c-38d5-4ae7-a987-5229442d5077) -->
![CropCare v2](https://github.com/carsonSgit/CropCare/assets/92652800/59fc84ea-f223-425e-ada7-e7569b46fd58)

### 📐 **App UML Diagrams**

#### User & Farm <a name="user-farm-uml"/>

```mermaid
classDiagram
    class User {
        - List<string> farmKeys
        + PropertyChangedEventHandler PropertyChanged
        + string Key
        + string Email
        + string Name
        + bool IsOwner
        + List<string> FarmKeys
        + bool IsAssigned
        + User(string email, string name, bool isOwner)
    }

    class Farm {
        + PropertyChangedEventHandler PropertyChanged
        + string Key
        + string Name
        + string DeviceId
        + EventHubConsumerClient Consumer
        - PlantController PlantController
        - SecurityController SecurityController
        - GeolocationController GeolocationController
        + Farm(string farmName, string deviceId)
    }

    class UserToFarm {
        + string Key
        + string UserId
        + string FarmId
        + UserToFarm(string userId, string farmId)
    }

    User "1" --> "0..*" UserToFarm
    Farm "1" --> "0..*" UserToFarm
```

#### Controllers <a name="controllers-uml"/>

```mermaid
classDiagram
    class BaseController {
        - string[] _readingTypes
        + Dictionary<string, ObservableCollection<Reading>> Readings
        + bool ValidateReading(Reading reading)
        + virtual void AddReading(Reading reading)
        + BaseController(string[] readingTypes)
    }

    class PlantController {
        - static readonly string[] _readingTypes
        + PropertyChangedEventHandler PropertyChanged
        + Reading Temperature
        + Reading Humidity
        + Reading Moisture
        + Reading WaterLevel
        + void ToggleFan()
        + void ToggleLed()
        + void AddReading(Reading reading)
        + PlantController()
        + string UpdateReadingHealthLabel(string sensorReading, char unitSymbol, double highThreshold, double lowThreshold)
        + string UpdateStateHealthLabel(string actuatorState)
    }

    class SecurityController {
        - static readonly string[] _readingTypes
        + PropertyChangedEventHandler PropertyChanged
        + Reading Loudness
        + Reading Motion
        + Reading Vibration
        + Reading Luminosity
        + void ToggleDoorLock()
        + void ToggleDoorOpen()
        + void AddReading(Reading reading)
        + SecurityController()
        + string UpdateReadingHealthLabel(string sensorReading, char unitSymbol, double highThreshold, double lowThreshold)
        + string UpdateStateHealthLabel(string actuatorState)
    }

    class GeolocationController {
        - static readonly string[] _readingTypes
        + PropertyChangedEventHandler PropertyChanged
        + Reading Latitude
        + Reading Longitude
        + Reading Pitch
        + Reading Roll
        + void ToggleBuzzer()
        + void AddReading(Reading reading)
        + GeolocationController()
    }

    BaseController <|-- PlantController
    BaseController <|-- SecurityController
    BaseController <|-- GeolocationController
```
