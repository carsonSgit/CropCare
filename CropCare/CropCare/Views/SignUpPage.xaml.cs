using CropCare.Services;
using Firebase.Auth;

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

	private async void Btn_SignUp_Clicked(object sender, EventArgs e)
	{
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
		{
            await DisplayAlert("Error", "Please enter your email and password", "OK");
            return;
        }
		if(Password != ConfirmPassword)
		{
            await DisplayAlert("Error", "Passwords must match", "OK");
            return;
        }
		if(Connectivity.NetworkAccess != NetworkAccess.Internet)
		{
            await DisplayAlert("Alert", "No internet connection", "OK");
            return;
        }
        try
        {
            AuthService.UserCreds = await AuthService.Client.CreateUserWithEmailAndPasswordAsync(Email, Password);
            await DisplayAlert("Success", "User signed up! ", "OK");
            

            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            await Shell.Current.GoToAsync($"//Index");

            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(ConfirmPassword));
        }
        catch (FirebaseAuthException ex)
        {
            await DisplayAlert("Alert", "An error occurred: " + ex.Message, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alert", "An error occurred: " + ex.Message, "OK");
        }
    }

	private async void Btn_Login_Clicked(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync($"//Login");
    }
}