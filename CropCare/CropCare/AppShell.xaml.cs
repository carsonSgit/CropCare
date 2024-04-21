namespace CropCare
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Set the app theme based on the user's preference
            var appTheme = Preferences.Get("apptheme", true);
            App.Current.UserAppTheme = appTheme ? AppTheme.Light : AppTheme.Dark;
        }
    }
}
