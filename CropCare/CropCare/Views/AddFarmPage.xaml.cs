namespace CropCare.Views;
using CropCare.Models;
using System.Collections.ObjectModel;

public partial class AddFarmPage : ContentPage
{
    public string FarmName { get; set; }
    public string FarmId { get; set; }
    public List<User> AssignedTechnicians { get; set; }
    public List<User> Technicians { get; set; }

    public delegate void UpdateFarmCollectionDelegate();
    private UpdateFarmCollectionDelegate UpdateFarmCollectionList;

    public AddFarmPage(UpdateFarmCollectionDelegate updateFarm)
	{
        UpdateFarmCollectionList = updateFarm;

        InitializeComponent();
        PopulateTechnicianPicker();
        AssignedTechnicians = new List<User>();
        BindingContext = this;
    }

    async private void PopulateTechnicianPicker()
    {
        this.Technicians = new List<User>();
        this.Technicians.AddRange((await App.Repo.UsersDb.GetItemsAsync(true)).Where(u => u.IsOwner == false));
    }

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue;
        var filteredTechnicians = this.Technicians.Where(t => t.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
        TechnicianCollectionView.ItemsSource = filteredTechnicians;
    }

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

    void DisplayFarmIDInfo(object sender, EventArgs e)
    {
        DisplayAlert("Information", "\"Farm ID\" is also referred to as the \"IOT device ID\".", "OK");
    }

    private async void OnAddFarmButtonClicked(object sender, EventArgs e)
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await DisplayAlert("No Internet", "Please check your internet connection", "OK");
            return;
        }

        if(string.IsNullOrEmpty(FarmName) || string.IsNullOrEmpty(FarmId))
        {
            await DisplayAlert("Error", "Please fill in all fields", "OK");
            return;
        }
        
        Farm newFarm = new Farm(FarmName, FarmId);
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