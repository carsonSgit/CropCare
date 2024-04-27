using CropCare.Models;
using CropCare.Models.Plant;

namespace CropCare.Views;

public partial class PlantPage : ContentPage
{
	private Farm Farm { get; set; }
    public PlantController PlantController { get; set; }
    public string Temperature => GetSensorReading(PlantController.Temperature);
    public string Humidity => GetSensorReading(PlantController.Temperature, 1);
    public string Moisture => GetSensorReading(PlantController.SoilMoisture);
    public string WaterLevel => GetSensorReading(PlantController.WaterLevel);

    public string Led
    {
        get => PlantController.Led.State;
        set => PlantController.Led.State = value;
    }

    public string Fan
    {
        get => PlantController.Fan.State;
        set => PlantController.Fan.State = value;
    }

    private string GetSensorReading(TemperatureHumidity sensor, int index = 0)
    {
        var reading = sensor.ReadSensor()[index];
        return $"{reading.Value}{reading.Unit}";
    }

    private string GetSensorReading(SoilMoisture sensor, int index = 0)
    {
        var reading = sensor.ReadSensor()[index];
        return $"{reading.Value}{reading.Unit}";
    }

    private string GetSensorReading(WaterLevel sensor, int index = 0)
    {
        var reading = sensor.ReadSensor()[index];
        return $"{reading.Value}{reading.Unit}";
    }

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
        temperature_measurementLbl.Text = Temperature;
        humidity_measurementLbl.Text = Humidity;
        moisture_measurementLbl.Text = Moisture;
        waterlvl_measurementLbl.Text = WaterLevel;

        led_measurementLbl.Text = Led;
        fan_measurementLbl.Text = Fan;
    }
    public void SetHealth()
    {
        UpdateHealthLabel(Temperature, temperature_healthLbl, '°', 30, 10);
        UpdateHealthLabel(Humidity, humidity_healthLbl, '%', 70, 30);
        UpdateHealthLabel(Moisture, moisture_healthLbl, 'o', 700, 200);
        UpdateHealthLabel(WaterLevel, waterlvl_healthLbl, 'w', 70, 30);
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