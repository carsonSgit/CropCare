using CropCare.Models;

namespace CropCare.Views;

public partial class SecurityPage : ContentPage
{
    private Farm Farm { get; set; }
    public SecurityPage(Farm farm)
	{
		InitializeComponent();
        Farm = farm;
        BindingContext = farm;
    }
}