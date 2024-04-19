using Firebase.Auth;
using CropCare.Services;
using System.Windows;
using static Java.Util.Jar.Attributes;

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
            //AuthService.UserCreds = await AuthService.Client.CreateUserWithEmailAndPasswordAsync(Email, Password);
            //App.CurrentUser.IsOwner = this.AccountType == "Owner";
            //await App.Repo.UsersDb.UpdateItemAsync(App.CurrentUser);
            AuthService.UserCreds = await AuthService.Client.CreateUserWithEmailAndPasswordAsync(Email, Password);
            Models.User user = new Models.User(Email, this.Name, this.AccountType == "Owner");

            await App.Repo.UsersDb.AddItemAsync(user);
            App.CurrentUser = user;

            Email = string.Empty;
            Password = string.Empty;
            await Shell.Current.GoToAsync($"//Index");

            await DisplayAlert("Success", "User signed up! ", "OK");
            //await DisplayAlert("Accountype", "KEVIN ADD THE ACCOUNT TYPE: " + this.AccountType, "Yes, I will");



            
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