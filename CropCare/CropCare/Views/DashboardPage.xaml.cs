using CropCare.Models;

namespace CropCare.Views;

public partial class DashboardPage : ContentPage
{
    private Farm Farm { get; set; }
    public DashboardPage(Farm farm)
	{
		InitializeComponent();
        Farm = farm;
        BindingContext = farm;
    }

    private void PlantNavigate_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PlantPage(Farm));
    }

    private void LocationNavigate_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LocationPage(Farm));
    }

    private void SecurityNavigate_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SecurityPage(Farm));
    }

    private void ControlsNavigate_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ControlPage(Farm));
    }
}