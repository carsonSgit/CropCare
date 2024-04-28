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
        Farm = farm;
        PlantController = new PlantController();
        BindingContext = farm;

        SetMeasurements();
        SetHealth();
    }

    public void SetMeasurements()
    {
        temperature_measurementLbl.Text = PlantController.GetTemperatureReading();
        humidity_measurementLbl.Text = PlantController.GetHumidityReading();
        moisture_measurementLbl.Text = PlantController.GetMoistureReading();
        waterlvl_measurementLbl.Text = PlantController.GetWaterLevelReading();

        led_measurementLbl.Text = PlantController.Led.State;
        fan_measurementLbl.Text = PlantController.Fan.State;
    }

    public void SetHealth()
    {
        PlantController.UpdateReadingHealthLabel(PlantController.GetTemperatureReading(), temperature_healthLbl, '°', 30, 10);
        PlantController.UpdateReadingHealthLabel(PlantController.GetHumidityReading(), humidity_healthLbl, '%', 70, 30);
        PlantController.UpdateReadingHealthLabel(PlantController.GetMoistureReading(), moisture_healthLbl, 'o', 700, 200);
        PlantController.UpdateReadingHealthLabel(PlantController.GetWaterLevelReading(), waterlvl_healthLbl, 'w', 70, 30);

        PlantController.UpdateStateHealthLabel(PlantController.Led.State, led_healthLbl);
        PlantController.UpdateStateHealthLabel(PlantController.Fan.State, fan_healthLbl);
    }
}
