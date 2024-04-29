namespace CropCare.Views;

using CropCare.Services;
using System.Text.RegularExpressions;

public partial class ResetPasswordPage : ContentPage
{
    public string Email { get; set; }
    public bool ModifyEmail {  get; set; }

    public ResetPasswordPage(string email, bool modifyEmail)
	{
		InitializeComponent();
		this.Email = email;
        this.ModifyEmail = modifyEmail;
		this.BindingContext = this;
        SetEmailEntryState();
	}

    private void SetEmailEntryState()
    {
        email_entry.IsEnabled = this.ModifyEmail;
    }

    private void OnEmailTextChanged(object sender, TextChangedEventArgs e)
    {
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