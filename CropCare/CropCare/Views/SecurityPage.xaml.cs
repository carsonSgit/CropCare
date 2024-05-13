using CropCare.Models;
using CropCare.Models.Security;

namespace CropCare.Views;

public partial class SecurityPage : ContentPage
{
    /// <summary>
    /// Gets the farm associated with the security controls.
    /// </summary>
    public Farm Farm { get; private set; }

    /// <summary>
    /// Gets the security controller associated with the page.
    /// </summary>
    public SecurityController SecurityController { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityPage"/> class.
    /// </summary>
    /// <param name="farm">The farm associated with the security controls.</param>
    public SecurityPage(Farm farm)
    {
        InitializeComponent();
        Farm = farm;
        SecurityController = farm.SecurityController;
        BindingContext = SecurityController;
    }

    private void doorLockSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        var command = doorLockSwitch.IsToggled ? Models.Command.ON : Models.Command.OFF;
        var newState = SecurityController.DoorLock.ControlActuator(command) ? command.ToString() : SecurityController.DoorLock.State;
        SecurityController.DoorLockState = SecurityController.UpdateStateHealthLabel(newState);

        if (command == Models.Command.ON)
            doorlock_status_circle.Color = Color.FromArgb("#1DBD40");
        else
            doorlock_status_circle.Color = Color.FromArgb("#DC2C2C");
    }

    private void doorOpenSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        var command = doorOpenSwitch.IsToggled ? Models.Command.ON : Models.Command.OFF;
        var newState = SecurityController.DoorOpener.ControlActuator(command) ? command.ToString() : SecurityController.DoorOpener.State;
        SecurityController.DoorOpenerState = SecurityController.UpdateStateHealthLabel(newState);

        if (command == Models.Command.ON)
            door_status_circle.Color = Color.FromArgb("#1DBD40");
        else
            door_status_circle.Color = Color.FromArgb("#DC2C2C");

    }
}