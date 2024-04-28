using CropCare.Models;

namespace CropCare.Views;

public partial class DashboardPage : ContentPage
{
    private Farm Farm { get; set; }
    public DashboardPage(Farm farm)
	{
		InitializeComponent();
        this.Farm = farm;
        BindingContext = this.Farm;
    }

    private void PlantNavigate_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PlantPage(Farm));
    }

    private async void LocationNavigate_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LocationPage(Farm));
    }

    private void SecurityNavigate_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SecurityPage(Farm));
    }

    private void TechnicianNavigate_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new TechnicianPage(Farm));
    }

    private void ControlsNavigate_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ControlPage(Farm));
    }
}