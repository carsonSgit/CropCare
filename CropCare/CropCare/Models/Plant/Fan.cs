﻿using CropCare.Interfaces;

namespace CropCare.Models.Plant
{
    // Team Name: CropCare
    // Team Members: Kevin Baggott, Cristiano Fazi and Carson Spriggs-Audet
    // Date: April 29th 2023, 6th Semester
    // Course Name: Application Development and Connected Objects
    // Description: Represents a fan used in plant-related systems.

    public class Fan : IActuator
    {
        /// <summary>
        /// Gets or sets the state of the fan.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fan"/> class.
        /// </summary>
        /// <param name="state">The initial state of the fan (default is OFF).</param>
        public Fan(Command state = Command.OFF)
        {
            State = state.ToString();
        }

        /// <summary>
        /// Controls the fan by sending a command to the IoT hub.
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