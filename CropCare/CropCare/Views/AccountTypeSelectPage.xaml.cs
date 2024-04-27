using Firebase.Auth;
using CropCare.Services;
using System.Windows;

namespace CropCare.Views;

public partial class AccountTypeSelectPage : ContentPage
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string AccountType { get; set; }

    public AccountTypeSelectPage(string email, string password, string name)
	{
        this.Email = email;
        this.Password = password;
        this.Name = name;

		InitializeComponent();
	}

    private async void Btn_Register_Clicked(object sender, EventArgs e)
    {
        try
        {
            if(Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("Alert", "No internet connection", "OK");
                return;
            }

            AuthService.UserCreds = await AuthService.Client.CreateUserWithEmailAndPasswordAsync(Email, Password);
            Models.User user = new Models.User(Email, this.Name, this.AccountType == "Owner", new List<string>());

            await App.Repo.UsersDb.AddItemAsync(user);
            App.CurrentUser = user;

            Email = string.Empty;
            Password = string.Empty;
            await Shell.Current.GoToAsync($"//Index");

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


    private async void OnTechnicienCardTapped(object sender, EventArgs e)
    {
        technicianCard.BorderColor = Colors.Black;
        technicianShadow.Opacity = 1;
        ownerCard.BorderColor = Colors.Transparent;
        ownerShadow.Opacity = 0;
        this.AccountType = "Technician";
    }

    private async void OnOwnerCardTapped(object sender, EventArgs e)
    {
        ownerCard.BorderColor = Colors.Black;
        ownerShadow.Opacity = 1;
        technicianCard.BorderColor = Colors.Transparent;
        technicianShadow.Opacity = 0;
        this.AccountType = "Owner";
    }

}