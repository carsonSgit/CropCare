using CropCare.Models;
using CropCare.Constants;
using System.Reflection.Metadata;

namespace CropCare.Views;

public partial class FarmSettingsPage : ContentPage
{
    /// <summary>
    /// Gets or sets the name of the farm.
    /// </summary>
    public string FarmName { get; set; }

    /// <summary>
    /// Gets or sets the icon of the farm.
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// Gets or sets the farm
    /// </summary>
    public Farm Farm { get; set; }

    public FarmSettingsPage(Farm farm)
	{
		InitializeComponent();
        this.Farm = farm;
        this.Icon = farm.IconPath;
        this.FarmName = farm.Name;
        PopulateIconPicker();
        //IconPicker.SelectedItem = farm.IconPath;
        BindingContext = this;
    }

    private void PopulateIconPicker()
    {
        foreach (var key in Constants.Constants.Icons.Keys)
        {
            IconPicker.Items.Add(key);

            if (this.Icon == Constants.Constants.GetIconPath(key))
                IconPicker.SelectedItem = key;
        }
    }

    private async void OnSaveChangesButtonClicked(object sender, EventArgs e)
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlert("No Internet", "Please check your internet connection", "OK");
            return;
        }

        if (string.IsNullOrEmpty(FarmName) || string.IsNullOrEmpty(IconPicker.SelectedItem as string))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        bool answer = await DisplayAlert("Confirm Update", "Are you sure you want to update this farm?", "Yes", "No");

        if (!answer)
            return;

        this.Farm.IconPath = Constants.Constants.GetIconPath(IconPicker.SelectedItem as string);
        this.Farm.Name = this.FarmName;
        await App.Repo.FarmsDb.UpdateItemAsync(this.Farm);

        await Navigation.PopAsync();
    }

    private async void OnDeleteFarmButtonClicked(object sender, EventArgs e)
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlert("No Internet", "Please check your internet connection", "OK");
            return;
        }

        bool answer = await DisplayAlert("Confirm Deletion", "Are you sure you want to delete this farm?", "Yes", "No");

        if (!answer)
            return;

        try
        {
            await App.Repo.UserToFarmDb.DeleteItemAsync(new UserToFarm(App.CurrentUser.Key, this.Farm.Key));
            await App.Repo.FarmsDb.DeleteItemAsync(this.Farm);
            // as this page is only accessible from a farm dashboard, we pop to root since the farm no longer exists
            await Navigation.PopToRootAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}