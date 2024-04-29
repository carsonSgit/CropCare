using CommunityToolkit.Maui.Core.Extensions;
using CropCare.Models;

namespace CropCare.Views;

public partial class TechnicianPage : ContentPage
{
    public List<User> AssignedTechnicians { get; set; }
    public List<User> Technicians { get; set; }
    public Farm Farm;

    public TechnicianPage(Farm farm)
	{
        //UpdateFarmCollectionList = updateFarm;

        InitializeComponent();
        AssignedTechnicians = new List<User>();
        this.Farm = farm;
        BindingContext = this;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        PopulateTechnicianPicker();
    }

    async private void PopulateTechnicianPicker()
    {
        this.Technicians = new List<User>();
        this.Technicians.AddRange((await App.Repo.UsersDb.GetItemsAsync(true)).Where(u => u.IsOwner == false));//Should I use get Items Async here?
        //this.Technicians.AddRange(App.Repo.UsersDb.Items.Where(u => u.IsOwner == false));//Or is it better to use Items?

        //Check users that are attachted to this farm
        var UserKeys = App.Repo.UserToFarmDb.Items.Where(u => u.FarmId == this.Farm.Key).Select(u => u.UserId).ToList();//Select all Users that match the farm ID
        this.AssignedTechnicians = App.Repo.UsersDb.Items.Where(u => UserKeys.Contains(u.Key) && u.IsOwner == false).ToList();//If the user is linked to the farm add them
        this.Technicians.ForEach(u => u.IsAssigned = this.AssignedTechnicians.Any(t => t.Key == u.Key));//This is inefficient consider changing --> Use a for loop to add to the list and checkmark at the same time
    }


    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)// Update the DB here or add a button?
    {
        var searchText = e.NewTextValue;
        var filteredTechnicians = this.Technicians.Where(t => t.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();

        TechnicianCollectionView.ItemsSource = filteredTechnicians;
    }

    private async void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkBox = (CheckBox)sender;
        var user = (User)checkBox.BindingContext;


        if (e.Value)
        {
            if(App.Repo.UserToFarmDb.Items.FirstOrDefault(f => f.UserId == user.Key && f.FarmId == Farm.Key) is not UserToFarm)
            {
                this.AssignedTechnicians.Add(user);
                await App.Repo.UserToFarmDb.AddItemAsync(new UserToFarm(user.Key, this.Farm.Key));
                user.IsAssigned = true;
            }
        }
        else
        {
            this.AssignedTechnicians.Remove(user);
            UserToFarm connection = App.Repo.UserToFarmDb.Items.FirstOrDefault(f => f.UserId == user.Key && f.FarmId == Farm.Key);
            await App.Repo.UserToFarmDb.DeleteItemAsync(connection);
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

        /*
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
        */
    }
}