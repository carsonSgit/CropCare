using CropCare.Models;
using CropCare.Models.Geolocation;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;
namespace CropCare.Views;

public partial class LocationPage : ContentPage
{
    private GeolocationController GeolocationController { get; set; }
    private Farm Farm { get; set; }
    public LocationPage(Farm farm)
	{
		InitializeComponent();
        Farm = farm;
        GeolocationController = farm.GeolocationController;
        BindingContext = GeolocationController;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();


        MainThread.BeginInvokeOnMainThread(async () =>
        {
            // Initialization issue, since we use map in a stack nav, for some
            // reason there is a data race and the map wont change default location
            await Task.Delay(1000);

            if (map.IsVisible)
            {
                Location location = new Location(GeolocationController.Latitude(), GeolocationController.Longitude());
                MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);
                map.MoveToRegion(mapSpan);
                map.Pins.Add(new Pin
                {
                    Label = Farm.Name,
                    Type = PinType.Place,
                    Location = location
                });
            }
        });
    }

    private void buzzer_Toggled(object sender, ToggledEventArgs e)
    {
        var state = (Switch)sender;

        if (state.IsToggled)
        {
            GeolocationController.Buzzer.ControlActuator(Models.Command.ON);
        }
        else
        {
            GeolocationController.Buzzer.ControlActuator(Models.Command.OFF);
        }
    }
}