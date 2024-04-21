namespace CropCare
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            var appTheme = Preferences.Get("apptheme", true);
            App.Current.UserAppTheme = appTheme ? AppTheme.Light : AppTheme.Dark;
        }
    }
}
