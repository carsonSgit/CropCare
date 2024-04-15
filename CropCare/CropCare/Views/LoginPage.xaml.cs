namespace CropCare.Views;

public partial class LoginPage : ContentPage
{
	public string Email { get; set; }
	public string Password { get; set; }

	public LoginPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	private async void LoginButton_Clicked(object sender, EventArgs e)
	{
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
		{
            await DisplayAlert("Error", "Please enter your email and password", "OK");
            return;
        }
	}
} 