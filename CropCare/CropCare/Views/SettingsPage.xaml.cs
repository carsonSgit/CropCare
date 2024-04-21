using CropCare.Services;
using Firebase.Auth;

namespace CropCare.Views;

public partial class SettingsPage : ContentPage
{
	public string Name { get; set; } = App.CurrentUser.Name;

	public SettingsPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        btn_update.IsVisible = Name != App.CurrentUser.Name;
    }

	private async void Btn_Update_Clicked(object sender, EventArgs e)
	{
        if(Connectivity.NetworkAccess != NetworkAccess.Internet)
		{
            await DisplayAlert("Alert", "No internet connection", "OK");
            return;
        }

        try
        {
            if (Name != App.CurrentUser.Name)
            {
                App.CurrentUser.Name = Name;
                await App.Repo.UsersDb.UpdateItemAsync(App.CurrentUser);
            }

            await DisplayAlert("Success", "User updated! ", "OK");
        }
        catch (FirebaseAuthException ex)
        {
            await DisplayAlert("Alert", "Could not update user: " + ex.Reason, "OK");
        }
        catch
        {
            await DisplayAlert("Alert", "An unexpected error occurred, please try again later.", "OK");
        }
    }

    private async void Btn_ChangePassword_Clicked(object sender, EventArgs e)
    {
        await AuthService.Client.ResetEmailPasswordAsync(App.CurrentUser.Email);
        await DisplayAlert("Password Reset", "An email to reset password was sent to " + App.CurrentUser.Email, "OK");
    }

}