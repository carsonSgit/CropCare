using CropCare.Models;
using CropCare.Models.Geolocation;
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

    /// <summary>
    /// Initializea new instance of the LocationPage class.
    /// </summary>
    /// <param name="farm">The farm whose location is to be displayed.</param>
    public LocationPage(Farm farm)
	{
		InitializeComponent();
        Farm = farm;
        GeolocationController = farm.GeolocationController;
        BindingContext = GeolocationController;
        UpdateBuzzerFrameVisibility();
    }

    private void UpdateBuzzerFrameVisibility()
    {
        var buzzerFrame = this.FindByName<Frame>("BuzzerFrame");
        buzzerFrame.IsVisible = App.CurrentUser.IsOwner;
    }

    [Obsolete]
    protected override void OnAppearing()
    {
        base.OnAppearing();

        Device.BeginInvokeOnMainThread(async () =>
        {
            await Task.Delay(200);

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