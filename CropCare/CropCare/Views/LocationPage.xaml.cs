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

    private void UpdateCharts(string s = null, string s2 = null)
    {
        PitchChart.BindingContext = GeolocationController.Charts[ReadingType.PITCH];
        RollChart.BindingContext = GeolocationController.Charts[ReadingType.ROLL];
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ChartPicker.SelectedIndex = 0;
        UpdateCharts();

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

    private void ChartPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        PitchChart.IsVisible = selectedIndex == 0;
        RollChart.IsVisible = selectedIndex == 1;
    }
}