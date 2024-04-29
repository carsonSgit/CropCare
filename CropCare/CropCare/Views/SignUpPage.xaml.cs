using Firebase.Auth;

namespace CropCare.Views;

/// <summary>
/// Represents a page for user sign-up.
/// </summary>
public partial class SignUpPage : ContentPage
{
    /// <summary>
    /// Gets or sets the email for the sign-up.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password for the sign-up.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the confirmation password for the sign-up.
    /// </summary>
    public string ConfirmPassword { get; set; }

    /// <summary>
    /// Gets or sets the name for the sign-up.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SignUpPage"/> class.
    /// </summary>
    public SignUpPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Email = string.Empty;
        Name = string.Empty;
        Password = string.Empty;
        ConfirmPassword = string.Empty;
        OnPropertyChanged(nameof(Email));
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Password));
        OnPropertyChanged(nameof(ConfirmPassword));
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

            await Navigation.PushAsync(new AccountTypeSelectPage(this.Email, this.Password, this.Name));

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