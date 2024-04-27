using CropCare.Services;
using Firebase.Auth;
using System.Linq;
namespace CropCare.Views;

public partial class LoginPage : ContentPage
{
    public string Name { get; set; }
    public string Email { get; set; }
	public string Password { get; set; }
    public UserCredential User { get; set; }

	public LoginPage()
	{
		InitializeComponent();
        User = AuthService.UserCreds;
		BindingContext = this;
	}

	private async void Btn_Login_Clicked(object sender, EventArgs e)
	{
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
		{
            await DisplayAlert("Error", "Please enter your email and password", "OK");
            return;
        }

        if(Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlert("Alert", "No internet connection", "OK");
            return;
        }

        try
        {
            AuthService.UserCreds = await AuthService.Client.SignInWithEmailAndPasswordAsync(Email, Password);
            App.CurrentUser = (await App.Repo.UsersDb.GetItemsAsync(true)).FirstOrDefault(u => u.Email == Email);

            await DisplayAlert("Success", $"{App.CurrentUser.Name} logged in! ", "OK");
            await Shell.Current.GoToAsync($"//OverviewPage");

        }
        catch (FirebaseAuthException ex)
        {
            await DisplayAlert("Alert", "Wrong email or password.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alert", "An error occurred: " + ex.Message, "OK");
        }
    }

    private async void Btn_Logout_Clicked(object sender, EventArgs e)
    {
        try
        {
            //Update UI
            AuthService.Client.SignOut();
            Email = string.Empty;
            Password = string.Empty;
            lblUser.Text = string.Empty;
            await Shell.Current.GoToAsync($"//Login");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alert", ex.Message, "OK");
        }

        OnPropertyChanged(nameof(Email));
        OnPropertyChanged(nameof(Password));
    }

    private async void Btn_SignUp_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//Signup");
    }
} 