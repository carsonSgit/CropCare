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