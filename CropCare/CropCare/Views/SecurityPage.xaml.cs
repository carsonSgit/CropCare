using CropCare.Models;
using CropCare.Models.Security;
using PropertyChanged;
using System.ComponentModel;

namespace CropCare.Views;
public partial class SecurityPage : ContentPage, INotifyPropertyChanged
{
    /// <summary>
    /// Gets the farm associated with the security controls.
    /// </summary>
    public Farm Farm { get; private set; }

    /// <summary>
    /// Gets the security controller associated with the page.
    /// </summary>
    public SecurityController SecurityController { get; private set; }

    public bool MotionReading { get; set; }

    public double LoudnessReading { get; set; }
    public string LoudnessUnit { get; set; }

    public bool VibrationReading { get; set; }

    public double LuminosityReading { get; set; }
    public string LuminosityUnit { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityPage"/> class.
    /// </summary>
    /// <param name="farm">The farm associated with the security controls.</param>
    public SecurityPage(Farm farm)
    {
        InitializeComponent();
        Farm = farm;
        SecurityController = farm.SecurityController;
        MotionReading = SecurityController.MotionSensor.Motion;
        LoudnessReading = SecurityController.LoudnessSensor.Loudness;
        LoudnessUnit = SecurityController.LoudnessSensor.LoudnessUnit;
        VibrationReading = SecurityController.VibrationSensor.Vibration;
        LuminosityReading = SecurityController.LuminositySensor.Luminosity;
        LuminosityUnit = SecurityController.LuminositySensor.LuminosityUnit;
        BindingContext = this;
    }

    private void doorLockSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        //var command = doorLockSwitch.IsToggled ? Models.Command.ON : Models.Command.OFF;
        //var newState = SecurityController.DoorLock.ControlActuator(command) ? command.ToString() : SecurityController.DoorLock.State;
        //SecurityController.DoorLockState = SecurityController.UpdateStateHealthLabel(newState);
    }

    private void doorOpenSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        //var command = doorOpenSwitch.IsToggled ? Models.Command.ON : Models.Command.OFF;
        //var newState = SecurityController.DoorOpener.ControlActuator(command) ? command.ToString() : SecurityController.DoorOpener.State;
        //SecurityController.DoorOpenerState = SecurityController.UpdateStateHealthLabel(newState);
    }
}