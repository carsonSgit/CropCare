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
        UpdateHealthLabel(PlantController.GetTemperatureReading(), temperature_healthLbl, '°', 30, 10);
        UpdateHealthLabel(PlantController.GetHumidityReading(), humidity_healthLbl, '%', 70, 30);
        UpdateHealthLabel(PlantController.GetMoistureReading(), moisture_healthLbl, 'o', 700, 200);
        UpdateHealthLabel(PlantController.GetWaterLevelReading(), waterlvl_healthLbl, 'w', 70, 30);
    }

    private void UpdateHealthLabel(string sensorReading, Label healthLbl, char unitSymbol, double highThreshold, double lowThreshold)
    {
        double sensorValue;
        if (double.TryParse(sensorReading.Split(unitSymbol)[0], out sensorValue))
        {
            if (sensorValue > highThreshold)
            {
                healthLbl.Text = "High";
                healthLbl.TextColor = Colors.Red;
            }
            else if (sensorValue < lowThreshold)
            {
                healthLbl.Text = "Low";
                healthLbl.TextColor = Colors.Red;
            }
            else
            {
                healthLbl.Text = "Normal";
                healthLbl.TextColor = Colors.Green;
            }
        }
    }
}
