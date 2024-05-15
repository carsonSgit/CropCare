using CropCare.Models;
using CropCare.Models.Controllers;

namespace CropCare.Views;

/// <summary>
/// Represents a page for managing plant-related controls.
/// </summary>
public partial class PlantPage : ContentPage
{
    /// <summary>
    /// Gets the plant controller associated with the page.
    /// </summary>
    public PlantController PlantController { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlantPage"/> class.
    /// </summary>
    /// <param name="farm">The farm containing the plant controller.</param>
    public PlantPage(Farm farm)
    {
        InitializeComponent();
        PlantController = farm.PlantController;
        Chart1.BindingContext = PlantController.GetChart(ReadingType.TEMPERATURE, "Temperature", "Time", "Temperature (°C)");
        BindingContext = PlantController;
    }

    private void fanSwitch_Toggled(object sender, ToggledEventArgs e)
    {

        //var command = fanSwitch.IsToggled ? Models.Command.ON : Models.Command.OFF;
        //PlantController.Fan.ControlActuator(command);
        //PlantController.FanState = PlantController.UpdateStateHealthLabel(command.ToString());


//         if (command == Models.Command.ON)
//             fan_status_circle.Color = Color.FromArgb("#1DBD40");
//         else
//             fan_status_circle.Color = Color.FromArgb("#DC2C2C");
    }

    private void ledSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        //var command = ledSwitch.IsToggled ? Models.Command.ON : Models.Command.OFF;
        //PlantController.Led.ControlActuator(command);
        //PlantController.LedState = PlantController.UpdateStateHealthLabel(command.ToString());
//         if (command == Models.Command.ON)
//             led_status_circle.Color = Color.FromArgb("#1DBD40");
//         else
//             led_status_circle.Color = Color.FromArgb("#DC2C2C");
    }
}
