using CropCare.Models;

namespace CropCare.Views;

/// <summary>
/// Represents a page for adding a new farm.
/// </summary>
public partial class DashboardPage : ContentPage
{
    public Farm Farm { get; set; }
    public bool IsOwner { get; set; } = App.CurrentUser.IsOwner;

    /// <summary>
    /// Initializes a new instance of the DashboardPage class.
    /// </summary>
    /// <param name="farm">The farm to display on the dashboard.</param>
    public DashboardPage(Farm farm)
    {
        InitializeComponent();
        this.Farm = farm;
        BindingContext = this;
    }

    private async void PlantNavigate_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PlantPage(Farm));
    }

    private async void LocationNavigate_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LocationPage(Farm));
    }

    private async void SecurityNavigate_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SecurityPage(Farm));
    }

    private async void TechnicianNavigate_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TechnicianPage(Farm));
    }
}