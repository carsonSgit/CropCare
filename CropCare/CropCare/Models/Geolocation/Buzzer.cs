using CropCare.Interfaces;
using System.ComponentModel;

namespace CropCare.Models.Geolocation
{
    /// <summary>
    /// Represents a buzzer actuator.
    /// </summary>
    public class Buzzer : IActuator, INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the state of the buzzer.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Controls the buzzer actuator.
        /// </summary>
        /// <param name="command">The command to control the buzzer.</param>
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
        /// Initializes a new instance of the <see cref="Buzzer"/> class with the initial state set to OFF.
        /// </summary>
        public Buzzer()
        {
            State = Command.OFF.ToString();
        }
    }
}