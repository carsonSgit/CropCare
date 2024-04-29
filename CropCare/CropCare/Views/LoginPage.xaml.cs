using CropCare.Services;
using Firebase.Auth;
namespace CropCare.Views;

/// <summary>
/// Represents a page for user login.
/// </summary>
public partial class LoginPage : ContentPage
{
    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the user credentials.
    /// </summary>
    public UserCredential User { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginPage"/> class.
    /// </summary>
    public LoginPage()
	{
		InitializeComponent();
        User = AuthService.UserCreds;
        BindingContext = this;
	}

    // Logout
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Email = string.Empty;
        Password = string.Empty;
        OnPropertyChanged(nameof(Email));
        OnPropertyChanged(nameof(Password));
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

    private async void Btn_Naviagte_To_ResetPasswordPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ResetPasswordPage("", true));
    }

    private async void Btn_SignUp_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//Signup");
    }
} 