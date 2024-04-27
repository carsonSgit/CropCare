using CropCare.Models;

namespace CropCare.Views;

public partial class PlantPage : ContentPage
{
	private Farm Farm { get; set; }
    public PlantPage(Farm farm)
	{
		InitializeComponent();
        Farm = farm;
        BindingContext = farm;
    }
}