
using CropCare.Services;
using System.Windows.Input;

namespace CropCare
{
    public partial class AppShell : Shell
    {
        public ICommand LogoutCommand { get; }

        public AppShell()
        {
            InitializeComponent();
            // Initialize the Async Logout Command
            LogoutCommand = new Command(async () => await OnLogoutAsync());

            // Set the app theme based on the user's preference
            var appTheme = Preferences.Get("apptheme", true);
            App.Current.UserAppTheme = appTheme ? AppTheme.Light : AppTheme.Dark;
        }

        private async Task OnLogoutAsync()
        {
            // Give the User an Alert so they can confirm whether or not to logout
            var answer = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (answer)
            {
                try
                {
                    Shell.Current.FlyoutIsPresented = false;
                    AuthService.Client.SignOut();
                    await Shell.Current.GoToAsync($"//Login");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alert", ex.Message, "OK");
                }
            }
        }

        /* 
         * Required MenuItem Clicked Event Handler
         * 
         * Must exist as workaround for .NET MAUI Shell bug where Async Commands do
         * not get hit in the Shell MenuItem.
         */
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            await OnLogoutAsync();
        }
    }

}