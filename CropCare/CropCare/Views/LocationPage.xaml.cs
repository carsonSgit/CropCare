using CropCare.Models;

namespace CropCare.Views;

public partial class LocationPage : ContentPage
{
	private Farm Farm { get; set; }
    public LocationPage(Farm farm)
	{
		InitializeComponent();
        Farm = farm;
        BindingContext = farm;
    }
}