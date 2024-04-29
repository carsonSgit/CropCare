using Command = CropCare.Models.Command;

namespace CropCare.Interfaces
{
    /// <summary>
    /// Interface for actuator devices.
    /// </summary>
    public interface IActuator
    {
        /// <summary>
        /// Gets or sets the current state of the actuator.
        /// </summary>
        string State { get; set; }

        /// <summary>
        /// Controls the actuator based on the specified command.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>True if the actuator was successfully controlled; otherwise, false.</returns>
        bool ControlActuator(Command command);
    }
}