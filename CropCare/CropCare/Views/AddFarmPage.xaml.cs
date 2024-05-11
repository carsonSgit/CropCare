namespace CropCare.Views;
using CropCare.Models;
using CropCare.Constants;

/// <summary>
/// Represents a page for adding a new farm.
/// </summary>
public partial class AddFarmPage : ContentPage
{
    /// <summary>
    /// Gets or sets the name of the farm.
    /// </summary>
    public string FarmName { get; set; }

    /// <summary>
    /// Gets or sets the ID of the farm.
    /// </summary>
    public string FarmId { get; set; }

    /// <summary>
    /// Gets or sets the list of assigned technicians for the farm.
    /// </summary>
    public List<User> AssignedTechnicians { get; set; }

    /// <summary>
    /// Gets or sets the list of available technicians.
    /// </summary>
    public List<User> Technicians { get; set; }

    /// <summary>
    /// Represents a delegate for updating the farm collection list.
    /// </summary>
    /// <returns>Returns void.</returns>
    public delegate void UpdateFarmCollectionDelegate();

    private UpdateFarmCollectionDelegate UpdateFarmCollectionList;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddFarmPage"/> class.
    /// </summary>
    /// <param name="updateFarm">Delegate for updating the farm collection list.</param>
    public AddFarmPage(UpdateFarmCollectionDelegate updateFarm)
    {
        UpdateFarmCollectionList = updateFarm;

        InitializeComponent();
        PopulateTechnicianPicker();
        PopulateIconPicker();
        AssignedTechnicians = new List<User>();
        BindingContext = this;
    }

    /// <summary>
    /// Populates the technician picker with available technicians.
    /// </summary>
    async private void PopulateTechnicianPicker()
    {
        this.Technicians = new List<User>();
        this.Technicians.AddRange((await App.Repo.UsersDb.GetItemsAsync(true)).Where(u => u.IsOwner == false));
    }

    /// <summary>
    /// Handles the event when the search bar text is changed.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">The event arguments.</param>
    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue;
        var filteredTechnicians = this.Technicians.Where(t => t.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
        TechnicianCollectionView.ItemsSource = filteredTechnicians;
    }

    /// <summary>
    /// Handles the event when the check box state is changed.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">The event arguments.</param>
    private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkBox = (CheckBox)sender;
        var user = (User)checkBox.BindingContext;

        if (e.Value)
        {
            AssignedTechnicians.Add(user);
            user.IsAssigned = true;
        }
        else
        {
            AssignedTechnicians.Remove(user);
            user.IsAssigned = false;
        }
    }

    /// <summary>
    /// Displays information about the farm ID when clicked.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">The event arguments.</param>
    void DisplayFarmIDInfo(object sender, EventArgs e)
    {
        DisplayAlert("Information", "\"Farm ID\" is also referred to as the \"IOT device ID\".", "OK");
    }

    private void PopulateIconPicker()
    {
        // Populate the IconPicker with the keys from Constants.Icons
        foreach (var key in Constants.Icons.Keys)
        {
            IconPicker.Items.Add(key);
        }
    }

    /// <summary>
    /// Handles the event when the add farm button is clicked.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">The event arguments.</param>
    private async void OnAddFarmButtonClicked(object sender, EventArgs e)
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlert("No Internet", "Please check your internet connection", "OK");
            return;
        }

        if (string.IsNullOrEmpty(FarmName) || string.IsNullOrEmpty(FarmId) || string.IsNullOrEmpty(IconPicker.SelectedItem as string))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }

        string selectedIconPath = Constants.GetIconPath(IconPicker.SelectedItem as string);
        Farm newFarm = new Farm(FarmName, FarmId, selectedIconPath);
        await App.Repo.FarmsDb.AddItemAsync(newFarm);
        await App.Repo.UserToFarmDb.AddItemAsync(new UserToFarm(App.CurrentUser.Key, newFarm.Key));

        foreach (User technician in AssignedTechnicians.ToList())
        {
            await App.Repo.UserToFarmDb.AddItemAsync(new UserToFarm(technician.Key, newFarm.Key));
        }

        foreach (User user in AssignedTechnicians.ToList())
        {
            user.IsAssigned = false;
        }

        UpdateFarmCollectionList();

        await Navigation.PopAsync();
    }
}