using CropCare.Models;

namespace CropCare.Views;

public partial class ControlPage : ContentPage
{
    private Farm Farm { get; set; }
    public ControlPage(Farm farm)
	{
		InitializeComponent();
        Farm = farm;
        BindingContext = farm;
    }
}