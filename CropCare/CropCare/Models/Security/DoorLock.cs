using CropCare.Interfaces;

namespace CropCare.Models.Security
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a door lock actuator and sensor.
    public class DoorLock : IActuator, ISensor
    {
        /// <summary>
        /// Gets or sets the state of the door lock.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoorLock"/> class with the specified initial state.
        /// </summary>
        /// <param name="state">The initial state of the door lock.</param>
        public DoorLock(Command state = Command.OFF)
        {
            State = state.ToString();
        }

        /// <summary>
        /// Controls the door lock actuator.
        /// </summary>
        /// <param name="command">The command to control the door lock.</param>
        /// <returns>True if the actuator was successfully controlled; otherwise, false.</returns>
        public bool ControlActuator(Command command)
        {
            if (State == command.ToString())
                return false;

            // Send command to IoT hub here
            State = command.ToString();
            // Send command to IoT hub here

            return true;
        }

        /// <summary>
        /// Reads the state of the door lock sensor.
        /// </summary>
        /// <returns>A list of readings from the door lock sensor.</returns>
        public List<Reading> ReadSensor()
        {
            return new List<Reading>
            {
                new Reading(ReadingType.DOORLOCK, ReadingUnit.NONE, State)
            };
        }
    }
}