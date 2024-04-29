namespace CropCare.Views;

using CropCare.Services;
using System.Text.RegularExpressions;

/// <summary>
/// Represents a page for resetting the user's password.
/// </summary>
public partial class ResetPasswordPage : ContentPage
{
    /// <summary>
    /// Gets or sets the email associated with the password reset.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the email can be modified.
    /// </summary>
    public bool ModifyEmail { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResetPasswordPage"/> class.
    /// </summary>
    /// <param name="email">The email associated with the password reset.</param>
    /// <param name="modifyEmail">A value indicating whether the email can be modified.</param>
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
            instructions_btn.BackgroundColor = Color.FromArgb("#538D22");
        else
            instructions_btn.BackgroundColor = Colors.LightGray;
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