﻿using CropCare.Interfaces;

namespace CropCare.Models.Plant
{
    /// <summary>
    /// Represents an LED used in plant-related systems.
    /// </summary>
    public class Led : IActuator
    {
        /// <summary>
        /// Gets or sets the state of the LED.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Led"/> class.
        /// </summary>
        /// <param name="state">The initial state of the LED (default is OFF).</param>
        public Led(Command state = Command.OFF)
        {
            State = state.ToString();
        }

        /// <summary>
        /// Controls the LED by sending a command to the IoT hub.
        /// </summary>
        /// <param name="command">The command to be sent.</param>
        /// <returns>True if the control command was successfully sent; otherwise, false.</returns>
        public bool ControlActuator(Command command)
        {
            if (State == command.ToString())
                return false;

            // send command to IoT hub
            State = command.ToString();

            return true;
        }
    }
}