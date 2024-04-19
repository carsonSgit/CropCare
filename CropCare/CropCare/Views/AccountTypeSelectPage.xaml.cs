using Firebase.Auth;
using CropCare.Services;
using System.Windows;

namespace CropCare.Views;

public partial class AccountTypeSelectPage : ContentPage
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string AccountType { get; set; }

    public AccountTypeSelectPage(string email, string password)
	{
        this.Email = email;
        this.Password = password;

		InitializeComponent();
	}

    private async void Btn_SignUp_Clicked(object sender, EventArgs e)
    {
        try
        {
            //AuthService.UserCreds = await AuthService.Client.CreateUserWithEmailAndPasswordAsync(Email, Password);
            await DisplayAlert("Success", "User signed up! ", "OK");
            await DisplayAlert("Accountype", "KEVIN ADD THE ACCOUNT TYPE: " + this.AccountType, "Yes, I will");
            
            Email = string.Empty;
            Password = string.Empty;
            
            await Navigation.PopAsync();
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


    private async void OnTechnicienCardTapped(object sender, EventArgs e)
    {
        technicianCard.BorderColor = Colors.Black;
        technicianShadow.Opacity = 1;
        ownerCard.BorderColor = Colors.Transparent;
        ownerShadow.Opacity = 0;
        this.AccountType = "Technician";
        //ownerCard.Effects.Clear();
    }

    private async void OnOwnerCardTapped(object sender, EventArgs e)
    {
        ownerCard.BorderColor = Colors.Black;
        ownerShadow.Opacity = 1;
        technicianCard.BorderColor = Colors.Transparent;
        technicianShadow.Opacity = 0;
        this.AccountType = "Owner";
        //technicienCard.Effects.Clear();
    }

}