using Firebase.Auth;
using CropCare.Services;

namespace CropCare.Views;

/// <summary>
/// Represents a page for selecting the type of account.
/// </summary>
public partial class AccountTypeSelectPage : ContentPage
{
    /// <summary>
    /// Gets or sets the email associated with the account.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the name associated with the account.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the password associated with the account.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the type of the account.
    /// </summary>
    public string AccountType { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountTypeSelectPage"/> class.
    /// </summary>
    /// <param name="email">The email associated with the account.</param>
    /// <param name="password">The password associated with the account.</param>
    /// <param name="name">The name associated with the account.</param>
    public AccountTypeSelectPage(string email, string password, string name)
    {
        this.Email = email;
        this.Password = password;
        this.Name = name;
        AccountType = "Technician";

        InitializeComponent();
    }

    /// <summary>
    /// Handles the event when the registration button is clicked.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">The event arguments.</param>
    private async void Btn_Register_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Alert", "No internet connection", "OK");
                return;
            }

            AuthService.UserCreds = await AuthService.Client.CreateUserWithEmailAndPasswordAsync(Email, Password);
            Models.User user = new Models.User(Email, this.Name, this.AccountType == "Owner");

            await App.Repo.UsersDb.AddItemAsync(user);
            App.CurrentUser = user;

            Email = string.Empty;
            Password = string.Empty;
            await Shell.Current.GoToAsync($"//OverviewPage");

            await DisplayAlert("Success", "User signed up! ", "OK");

            Email = string.Empty;
            Password = string.Empty;

            await Navigation.PopAsync();
        }
        catch (FirebaseAuthException ex)
        {
            await DisplayAlert("Alert", "Could not create account: " + ex.Reason, "OK");
        }
        catch
        {
            await DisplayAlert("Alert", "An unexpected error occurred, please try again later.", "OK");
        }
    }

    /// <summary>
    /// Handles the event when the technician card is tapped.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">The event arguments.</param>
    private void OnTechnicianCardTapped(object sender, EventArgs e)
    {
        technicianCard.BorderColor = Colors.Black;
        technicianShadow.Opacity = 1;
        ownerCard.BorderColor = Colors.Transparent;
        ownerShadow.Opacity = 0;
        this.AccountType = "Technician";
    }

    /// <summary>
    /// Handles the event when the owner card is tapped.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">The event arguments.</param>
    private void OnOwnerCardTapped(object sender, EventArgs e)
    {
        ownerCard.BorderColor = Colors.Black;
        ownerShadow.Opacity = 1;
        technicianCard.BorderColor = Colors.Transparent;
        technicianShadow.Opacity = 0;
        this.AccountType = "Owner";
    }
}