using CropCare.Interfaces;
using CropCare.Models;
using CropCare.Models.Plant;

namespace CropCare.Views;

public partial class PlantPage : ContentPage
{
    private Farm Farm { get; set; }
    public PlantController PlantController { get; set; }

    public PlantPage(Farm farm)
    {
        InitializeComponent();
        PlantController = farm.PlantController;
        BindingContext = PlantController;
    }

    private void fanSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        var command = fanSwitch.IsToggled ? Models.Command.ON : Models.Command.OFF;
        PlantController.Fan.ControlActuator(command);
        PlantController.FanState = PlantController.UpdateStateHealthLabel(command.ToString());
    }

    private void ledSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        var command = ledSwitch.IsToggled ? Models.Command.ON : Models.Command.OFF;
        PlantController.Led.ControlActuator(command);
        PlantController.LedState = PlantController.UpdateStateHealthLabel(command.ToString());
    }
}
