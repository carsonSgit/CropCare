using CropCare.Models;
using CropCare.Models.Plant;

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

    public double WaterLevel { get; set; }
    public string WaterLevelUnit { get; set; }
    public double Temperature { get; set; }
    public string TemperatureUnit { get; set; }
    public double Humidity { get; set; }
    public string HumidityUnit { get; set; }
    public double SoilMoisture { get; set; }
    public string SoilMoistureUnit { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlantPage"/> class.
    /// </summary>
    /// <param name="farm">The farm containing the plant controller.</param>
    public PlantPage(Farm farm)
    {
        InitializeComponent();
        PlantController = farm.PlantController;
        WaterLevel = PlantController.WaterLevelSensor.WaterLevel;
        WaterLevelUnit = PlantController.WaterLevelSensor.WaterLevelUnit;
        Temperature = PlantController.TemperatureSensor.Temperature;
        TemperatureUnit = PlantController.TemperatureSensor.TemperatureUnit;
        Humidity = PlantController.TemperatureSensor.Humidity;
        HumidityUnit = PlantController.TemperatureSensor.HumidityUnit;
        SoilMoisture = PlantController.SoilMoistureSensor.Moisture;
        SoilMoistureUnit = PlantController.SoilMoistureSensor.MoistureUnit;
        BindingContext = this;
    }

    private void fanSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        //var command = fanSwitch.IsToggled ? Models.Command.ON : Models.Command.OFF;
        //PlantController.Fan.ControlActuator(command);
        //PlantController.FanState = PlantController.UpdateStateHealthLabel(command.ToString());
    }

    private void ledSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        //var command = ledSwitch.IsToggled ? Models.Command.ON : Models.Command.OFF;
        //PlantController.Led.ControlActuator(command);
        //PlantController.LedState = PlantController.UpdateStateHealthLabel(command.ToString());
    }
}
