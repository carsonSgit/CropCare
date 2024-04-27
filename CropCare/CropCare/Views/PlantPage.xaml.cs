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
}