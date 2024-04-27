using CropCare.Models;
using CropCare.Models.Plant;

namespace CropCare.Views;

public partial class PlantPage : ContentPage
{
	private Farm Farm { get; set; }
    public PlantController PlantController { get; set; }
    public string Temperature
    {
        get
        {
            var reading = PlantController.Temperature.ReadSensor()[0];
            return $"{reading.Value}{reading.Unit}";
        }
    }

    public string Humidity
    {
        get
        {
            var reading = PlantController.Temperature.ReadSensor()[1];
            return $"{reading.Value}{reading.Unit}";
        }
    }

    public string Moisture
    {
        get
        {
            var reading = PlantController.SoilMoisture.ReadSensor()[0];
            return $"{reading.Value}{reading.Unit}";
        }
    }
    public string WaterLevel
    {
        get
        {
            var reading = PlantController.WaterLevel.ReadSensor()[0];
            return $"{reading.Value}{reading.Unit}";
        }
    }

    public string Led { 
        get { return PlantController.Led.State; }
        set { PlantController.Led.State = value; }
    }
    public string Fan
    {
        get { return PlantController.Fan.State; }
        set { PlantController.Fan.State = value; }
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
        double temperatureValue;
        if (double.TryParse(Temperature.Split('°')[0], out temperatureValue))
        {
            if (temperatureValue > 30)
            {
                temperature_healthLbl.Text = "High";
                temperature_healthLbl.TextColor = Colors.Red;
            }
            else if(temperatureValue < 10)
            {
                temperature_healthLbl.Text = "Low";
                temperature_healthLbl.TextColor = Colors.Red;
            }
            else
            {
                temperature_healthLbl.Text = "Normal";
                temperature_healthLbl.TextColor = Colors.Green;
            }
        }
        double humidityValue;
        if (double.TryParse(Humidity.Split('%')[0], out humidityValue))
        {
            if (humidityValue > 70)
            {
                humidity_healthLbl.Text = "High";
                humidity_healthLbl.TextColor = Colors.Red;
            }
            else if (humidityValue < 30)
            {
                humidity_healthLbl.Text = "Low";
                humidity_healthLbl.TextColor = Colors.Red;
            }
            else
            {
                humidity_healthLbl.Text = "Normal";
                humidity_healthLbl.TextColor = Colors.Green;
            }
        }
        double moistureValue;
        if (double.TryParse(Moisture.Split('o')[0], out moistureValue))
        {
            if (moistureValue > 700)
            {
                moisture_healthLbl.Text = "High";
                moisture_healthLbl.TextColor = Colors.Red;
            }
            else if (moistureValue < 200)
            {
                moisture_healthLbl.Text = "Low";
                moisture_healthLbl.TextColor = Colors.Red;
            }
            else
            {
                moisture_healthLbl.Text = "Normal";
                moisture_healthLbl.TextColor = Colors.Green;
            }
        }
        double waterLevelValue;
        if (double.TryParse(WaterLevel.Split('w')[0], out waterLevelValue))
        {
            if (waterLevelValue > 70)
            {
                waterlvl_healthLbl.Text = "High";
                waterlvl_healthLbl.TextColor = Colors.Red;
            }
            else if (waterLevelValue < 30)
            {
                waterlvl_healthLbl.Text = "Low";
                waterlvl_healthLbl.TextColor = Colors.Red;
            }
            else
            {
                waterlvl_healthLbl.Text = "Normal";
                waterlvl_healthLbl.TextColor = Colors.Green;
            }
        }

    }
}