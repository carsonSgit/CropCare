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
        btn_update.TextColor = UpdateEnabled ? Colors.White : Colors.Black;
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

    private async void Btn_Naviagte_To_ResetPasswordPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ResetPasswordPage(App.CurrentUser.Email));
    }

    private void ThemeSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        if (ThemeSwitch.IsToggled)
            App.Current.UserAppTheme = AppTheme.Light;
        else
            App.Current.UserAppTheme = AppTheme.Dark;

        Preferences.Set("apptheme", ThemeSwitch.IsToggled);
    }
}