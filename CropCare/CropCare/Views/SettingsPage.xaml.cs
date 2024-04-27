using CropCare.Services;
using Firebase.Auth;

namespace CropCare.Views;

public partial class SettingsPage : ContentPage
{
    // Property to store the user's name
	public string Name { get; set; } = App.CurrentUser.Name;
    // Propterty to enable or disable the update button
    public bool UpdateEnabled { get; set; } = false;
    // Property to change the color of the update button
    public Color UpdateButtonColor { get; set; } = Colors.White;

    public SettingsPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Enable the update button if the name is different from the current saved user's name
        UpdateEnabled = Name != App.CurrentUser.Name;
        // Change the color of the update button to differentiate between enabled and disabled states
        UpdateButtonColor = UpdateEnabled ? Color.FromArgb("#538D22") : Colors.LightGray;
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

            UpdateEnabled = Name != App.CurrentUser.Name;
            UpdateButtonColor = UpdateEnabled ? Color.FromArgb("#538D22") : Colors.LightGray;
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

    // Method to handle the app theme switch
    private void ThemeSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        // Set the app theme based on the switch state
        if (ThemeSwitch.IsToggled)
        {
            // Set the app theme to light
            App.Current.UserAppTheme = AppTheme.Light;
        }
        else
        {
            // Set the app theme to dark
            App.Current.UserAppTheme = AppTheme.Dark;
        }
        // Save the app theme preference
        Preferences.Set("apptheme", ThemeSwitch.IsToggled);
    }
}