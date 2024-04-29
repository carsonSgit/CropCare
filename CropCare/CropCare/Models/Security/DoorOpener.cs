using CropCare.Interfaces;

namespace CropCare.Models.Security
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a door opener actuator.
    public class DoorOpener : IActuator
    {
        /// <summary>
        /// Gets or sets the state of the door opener.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoorOpener"/> class with the specified initial state.
        /// </summary>
        /// <param name="state">The initial state of the door opener.</param>
        public DoorOpener(Command state = Command.OFF)
        {
            State = state.ToString();
        }

        /// <summary>
        /// Controls the door opener actuator.
        /// </summary>
        /// <param name="command">The command to control the door opener.</param>
        /// <returns>True if the actuator was successfully controlled; otherwise, false.</returns>
        public bool ControlActuator(Command command)
        {
            if (State == command.ToString())
                return false;

            // Send command to IoT hub here
            State = command.ToString();

            return true;
        }
    }
}
