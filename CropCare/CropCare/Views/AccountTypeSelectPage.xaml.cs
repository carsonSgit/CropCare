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
        technicienCardShadow.Opacity = (float)0.6;
        ownerCardShadow.Opacity = 0;
        this.AccountType = "Technicien";
        //ownerCard.Effects.Clear();
    }

    private async void OnOwnerCardTapped(object sender, EventArgs e)
    {
        ownerCardShadow.Opacity = (float)0.6;
        technicienCardShadow.Opacity = 0;
        this.AccountType = "Owner";
        //technicienCard.Effects.Clear();
    }

}