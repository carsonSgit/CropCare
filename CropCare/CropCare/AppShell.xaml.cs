using System.Windows.Input;

namespace CropCare
{
    public partial class AppShell : Shell
    {
        public ICommand LogoutCommand { get; }

        public AppShell()
        {
            InitializeComponent();
            LogoutCommand = new Command(OnLogout);

            // Set the app theme based on the user's preference
            var appTheme = Preferences.Get("apptheme", true);
            App.Current.UserAppTheme = appTheme ? AppTheme.Light : AppTheme.Dark;
        }

        private void OnLogout()
        {
        }
    }
}