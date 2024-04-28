[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-24ddc0f5d75046c5622901739e7c5dd533143b0c8e959d652212380cedb1ea36.svg)](https://classroom.github.com/a/ZKGepmmw)
# <div align='center'> 420-6A6-AB Application Development III
# <div align='center'> 420-6P3-AB Connected Objects 
# <div align='center'> Winter 2024

### <div align='center'> Final Project

# 🌱 Functional Overview 
CropCare seeks to deliver timely delivery of relevant data, and allow users to remotely control key systems for containerized gardens. Leveraling build in hardware solutions, we can monitor the relevant plant data and control related hardware to ensure secure and efficient plant growth. Our mobile application will facilitate this remote control and will provide users with a visual interface with which to view the relevant data.

# 📋 Design Overview
Designing the CropCare Guardian app involves creating an intuitive and efficient user interface that caters to the specific needs of both crop owners and crop technicians. The app's design must facilitate easy navigation between its various functionalities while providing a seamless user experience. Below is a detailed overview of the app's design, including descriptions of each screen, screen design elements, and the navigation flow.

### Login page
This screen will allow users to sign in to their accounts. Additionally, if the user does not have an account, they can click a link that navigates them to the register page. The page will contain a username field, a password field and a sign in button. There will be input validation to ensure proper input before credentials entered are sent off to authenticate the user.

### Register page
This page will be used for account creation. A user will have to fill out an email field, a username field, two password fields to confirm they match and a register button. There will also be a link back to the login page in case a user wound up in the register page by mistake. Form input validation is important on this page as well. Once the register button is clicked and the inputs are validated, the user will be moved to the account setup page. 

### Account setup page
This page will be used to set a user account as a crop owner or crop technician. It is important to properly include the written difference between the two types of accounts so users can know which account type suits their requirements. It will be two big cards or buttons on the screen each containing the description of the account type. Once selected, a user can hit the “confirm” button to confirm the account type they need. 

## Overview page
This page will list all the containers a user is associated with. It will list its status as healthy, needs attention or in critical condition. The page will also have the ability to remove containers using a swipe view and when tapped will open up the dashboard of a container. A button will be placed in the bottom corner of the screen to add a container.

### Dashboard
The top of the dashboard will contain the status of your container. It will show the status as healthy, needs attention, or in critical condition. Below this status will be a section containing buttons that navigate to other pages. There will be a button that navigates to the monitoring page, a button that navigates to the control page, and a button that will bring you to the gps page (gps page is only displayed to crop owner).

### Monitor page
This page is a comprehensive dashboard that provides detailed information on the current state of the container's environment using our sensors. It will display all the important information about a container in a neat and pleasing way.

### Remote control page
This page empowers users to remotely control various components within the container, allowing for immediate adjustments to the environment based on the data observed on the Monitor Page.

### GPS page
This page is solely for crop owners. This page will have an interactive map of the world and will contain a marker of where their container is currently located.

### General Navigation and Design Notes:
In order to keep the screen less cluttered, a flyout menu will be used. This menu will contain the dashboard page, the settings page and a logout button. The items in this flyout menu will use shell navigation and all other pages will be navigated using stack navigation, the exception being the login and register page being shell navigation. 

### Settings page
This page will contain settings for the account of our users. These settings may include toggling app themes from a light mode to a dark mode using a switch button, as well as changing username, password, and email using text boxes.

# 📱 App Prototype
![image](https://github.com/JAC-Final-Project-W24-6A6-6P3/final-project-cropcare/assets/98350514/13b4257c-38d5-4ae7-a987-5229442d5077)

# 🧩 App UML Diagrams

## User & Farm 

```mermaid
classDiagram
    class Farm {
        + Key
        + Name
        + DeviceId
        + PropertyChanged
        + InvokeMethodAsync(methodName: MethodName, parametersJSON: string): Task
    }
    class User {
        + Key
        + Email
        + Name
        + IsOwner
        + FarmKeys
        + IsAssigned
        + PropertyChanged
    }
    class UserToFarm {
        + Key
        + UserId
        + FarmId
    }
    class IHasKey {
        <<interface>>
        + Key
    }

    Farm ..|> IHasKey
    User ..|> IHasKey
    UserToFarm ..|> IHasKey
    User -- Farm : Manages
    User *-- Farm : Has
```

## Plant Controller 

```mermaid
classDiagram
    class PlantController {
        + Fan
        + Led
        + SoilMoisture
        + TemperatureHumidity
        + WaterLevel
    }
    class Fan {
        - State: string
        + ControlActuator(command: Command): bool
    }
    class Led {
        - State: string
        + ControlActuator(command: Command): bool
    }
    class SoilMoisture {
        + ReadSensor(): List
        <Reading>
    }
    class TemperatureHumidity {
        + ReadSensor(): List
        <Reading>
    }
    class WaterLevel {
        + ReadSensor(): List
        <Reading>
    }
    class Reading {
        - Type: string
        - Unit: string
        - Value: string
    }
    
    PlantController "1" -- "many" Fan : Contains
    PlantController "1" -- "many" Led : Contains
    PlantController "1" -- "many" SoilMoisture : Contains
    PlantController "1" -- "many" TemperatureHumidity : Contains
    PlantController "1" -- "many" WaterLevel : Contains
    SoilMoisture "1" -- "many" Reading : Generates
    TemperatureHumidity "1" -- "many" Reading : Generates
    WaterLevel "1" -- "many" Reading : Generates
```

## Security Controller

```mermaid
classDiagram
    class DoorLock {
        - State: string
        + ControlActuator(command: Command): bool
        + ReadSensor(): List
        <Reading>
    }
    class DoorOpener {
        - State: string
        + ControlActuator(command: Command): bool
    }
    class Loudness {
        + ReadSensor(): List
        <Reading>
    }
    class Luminosity {
        + ReadSensor(): List
        <Reading>
    }
    class Motion {
        + ReadSensor(): List
        <Reading>
    }
    class SecurityController {
        + Loudness
        + Motion
        + Vibration
        + DoorLock
        + DoorOpener
        + Luminosity
    }
    class Vibration {
        + ReadSensor(): List
        <Reading>
    }
    class Reading {
        - Type: string
        - Unit: string
        - Value: string
    }
    
    SecurityController "1" -- "many" Loudness : Contains
    SecurityController "1" -- "many" Motion : Contains
    SecurityController "1" -- "many" Vibration : Contains
    SecurityController "1" -- "many" DoorLock : Contains
    SecurityController "1" -- "many" DoorOpener : Contains
    SecurityController "1" -- "many" Luminosity : Contains
    Loudness "1" -- "many" Reading : Generates
    Luminosity "1" -- "many" Reading : Generates
    Motion "1" -- "many" Reading : Generates
    Vibration "1" -- "many" Reading : Generates
    DoorLock "1" -- "many" Reading : Generates
```

## Geolocation Controller
```mermaid
classDiagram
    class Accelerometer {
        + ReadSensor(): List
        <Reading>
    }
    class Buzzer {
        - State: string
        + ControlActuator(command: Command): bool
    }
    class GeolocationController {
        + Buzzer
        + GPS
        + Accelerometer
    }
    class GPS {
        + ReadSensor(): List
        <Reading>
    }
    class Reading {
        - Type: string
        - Unit: string
        - Value: string
    }
    
    GeolocationController "1" -- "many" Buzzer : Contains
    GeolocationController "1" -- "many" GPS : Contains
    GeolocationController "1" -- "many" Accelerometer : Contains
    Accelerometer "1" -- "many" Reading : Generates
    GPS "1" -- "many" Reading : Generates
    Buzzer "1" -- "many" Reading : Generates
```


# 🛠️ App Features

Hardware Features
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

Cloud Computing Features
- Data Storage
  - Create a Cosmos storage account
  - Create a Cosmos Database to store the container information
  - Create all necessary containers
  - Create azure keyvault to store connection strings

- Web App API
  - Create endpoints to access logging information from Cosmos DB
  - Create endpoints to control container functions remotely

Mobile App Features
- User Authentication
  - Build a user login/signup page
  - Create a user login system
  - Create a user sign-up system (Considering the two account types)
  - Implement a logout button
  - Ensure app differentiates between account types

- Supporting Infrastructure
  - Create a navigation bar and nested flyout menus
  - Create a logo for the application and company
  - Access environment variables

- Monitoring Page
  - Display all the data relating to the plants, such as moisture, and water levels through an interactive page
  - Alert fleet owner if there are security breaches/issues

- Remote Control Page
  - Display container state information, keeping in mind the current users authorization level, 
  - Allow users to control systems of the container farm, like the door or lights. Only allowing the user to control systems their authorization will allow.

- GPS Monitoring Page
  - Fleet manager should see a map with all their farms
  - A list of farms with their coordinates should be available
  - Should have a button where fleet owners can register new farms

- Settings Page
  - Design Settings Page
  - Allow user to change account details
  - Allow users to change color theme


# 🤩 Potential Showstoppers
### Color Theme
To address the unique needs of every user and allow for a certain amount of customization, we wish to implement a color switching option into our application. This will allow users to choose between light and dark themes enabling them to pick what fits best for them.

### Long Term Data Storage
By leveraging azure's cosmos database, we can store monitoring data long term. While the IOT hub only stores data for a short period of time at specific times, cosmos can store data forever, allowing us to display historical data to our users.

### Container Health Level
We want to show the health of the farm of the user in the farm overview, allowing users to quickly assess which farms require attention at a glance. This feature will prevent users from having to click on each farm and go into them and see which need attention.

# 🌐 Link to Our Document
https://docs.google.com/document/d/1CgeMB0Ia7MkWsxPN-nKMrvUxbPpmW7MrcGwJhg-tKfA/edit?usp=sharing
