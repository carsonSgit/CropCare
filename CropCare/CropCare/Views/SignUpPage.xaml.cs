namespace CropCare.Views;

public partial class SignUpPage : ContentPage
{
	public string Email { get; set; }
	public string Password { get; set; }
	public string ConfirmPassword { get; set; }

	public SignUpPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	private async void SignUpButton_Clicked(object sender, EventArgs e)
	{
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
		{
            await DisplayAlert("Error", "Please enter your email and password", "OK");
            return;
        }
    }

	private async void LoginButton_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new LoginPage());
	}
}