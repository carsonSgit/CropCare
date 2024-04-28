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
        SecurityController = new SecurityController();
        BindingContext = SecurityController;
    }

    private void doorLockSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        var command = doorLockSwitch.IsToggled ? Models.Command.ON : Models.Command.OFF;
        SecurityController.DoorLock.ControlActuator(command);
        SecurityController.DoorLockState = SecurityController.UpdateStateHealthLabel(command.ToString());
    }

    private void doorOpenSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        var command = doorOpenSwitch.IsToggled ? Models.Command.ON : Models.Command.OFF;
        SecurityController.DoorOpener.ControlActuator(command);
        SecurityController.DoorOpenerState = SecurityController.UpdateStateHealthLabel(command.ToString());
    }
}