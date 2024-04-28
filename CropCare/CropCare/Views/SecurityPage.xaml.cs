using CropCare.Models;
using CropCare.Models.Security;

namespace CropCare.Views;

public partial class SecurityPage : ContentPage
{
    private Farm Farm { get; set; }
    private SecurityController SecurityController { get; set; } 
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
    }

    private void doorOpenSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        var command = doorOpenSwitch.IsToggled ? Models.Command.ON : Models.Command.OFF;
        var newState = SecurityController.DoorOpener.ControlActuator(command) ? command.ToString() : SecurityController.DoorOpener.State;
        SecurityController.DoorOpenerState = SecurityController.UpdateStateHealthLabel(newState);
    }
}