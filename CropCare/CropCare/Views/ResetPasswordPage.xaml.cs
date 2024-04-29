namespace CropCare.Views;

using CropCare.Services;
using System.Text.RegularExpressions;
using static Java.Util.Jar.Attributes;

public partial class ResetPasswordPage : ContentPage
{
    public string Email { get; set; }

    public ResetPasswordPage(string email)
	{
		InitializeComponent();
		this.Email = email;
		this.BindingContext = this;
	}



    private void OnEmailTextChanged(object sender, TextChangedEventArgs e)
    {
        /*
        // Enable the update button if the name is different from the current saved user's name
        UpdateEnabled = Name != App.CurrentUser.Name;
        // Change the color of the update button to differentiate between enabled and disabled states
        UpdateButtonColor = UpdateEnabled ? Color.FromArgb("#538D22") : Colors.LightGray;
        btn_update.TextColor = UpdateEnabled ? Colors.White : Colors.Black;
        */

        bool isValidEmail = IsValidEmail(e.NewTextValue);
        instructions_btn.IsEnabled = isValidEmail;
        if (isValidEmail)
        {
            instructions_btn.BackgroundColor = Color.FromArgb("#538D22");
        }
        else
        {
            instructions_btn.BackgroundColor = Colors.LightGray;
        }
    }

    private bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
    }

    private async void Btn_ChangePassword_Clicked(object sender, EventArgs e)
    {
        await AuthService.Client.ResetEmailPasswordAsync(this.Email);
        await DisplayAlert("Password Reset", "An email to reset password was sent to " + App.CurrentUser.Email, "OK");
    }
}