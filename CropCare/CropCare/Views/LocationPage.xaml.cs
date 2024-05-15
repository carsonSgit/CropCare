using CropCare.Models;
using CropCare.Models.Controllers;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
namespace CropCare.Views;

/// <summary>
/// Represents a page for displaying the location of a farm.
/// </summary>
public partial class LocationPage : ContentPage
{
    private GeolocationController GeolocationController { get; set; }
    private Farm Farm { get; set; }
    public double Pitch { get; set; }
    public double Roll { get; set; }
    /// <summary>
    /// Initializea new instance of the LocationPage class.
    /// </summary>
    /// <param name="farm">The farm whose location is to be displayed.</param>
    public LocationPage(Farm farm)
    {
        InitializeComponent();
        GeolocationController = farm.GeolocationController;
        Farm = farm;
        App.IOTService.MessageReceived += UpdateCharts;
        BindingContext = GeolocationController;
    }
    private void UpdateCharts(string s, string s2)
    {
        PitchChart.BindingContext = GeolocationController.Charts[ReadingType.PITCH];
        RollChart.BindingContext = GeolocationController.Charts[ReadingType.ROLL];
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
                Location location = new Location(double.Parse(GeolocationController.Latitude.Value), double.Parse(GeolocationController.Longitude.Value));
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
        //var state = (Switch)sender;
        
        //if (state.IsToggled)
        //{
        //    GeolocationController.Buzzer.ControlActuator(Models.Command.ON);
        //    buzzer_status_circle.Color = Color.FromArgb("#1DBD40");
        //}
        //else
        //{
        //    GeolocationController.Buzzer.ControlActuator(Models.Command.OFF);
        //    buzzer_status_circle.Color = Color.FromArgb("#DC2C2C");
        //}
    }


}