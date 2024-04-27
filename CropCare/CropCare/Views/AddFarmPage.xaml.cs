namespace CropCare.Views;
using CropCare.Models;
using System.Collections.ObjectModel;

public partial class AddFarmPage : ContentPage
{
    public string FarmName { get; set; }
    public string FarmId { get; set; }
    public List<User> AssignedTechnicians { get; set; }
    public List<User> Technicians { get; set; }
    public ObservableCollection<Farm> FarmsCollection { get; set; }

    public AddFarmPage()
	{
        FarmsCollection = App.Repo.FarmsDb.Items;

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

    private async void OnPlusIconClicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        var user = (User)button.CommandParameter;

        await DisplayAlert("Technicien Added ", $"{user} was added", "OK");
        this.Technicians.Remove(user);
        TechnicianCollectionView.ItemsSource = this.Technicians;
        AssignedTechnicians.Add(user);
    }

    private async void OnAddFarmButtonClicked(object sender, EventArgs e)
    {
        Farm newFarm = new Farm("test");
        await App.Repo.FarmsDb.AddItemAsync(newFarm);
        App.CurrentUser.FarmKeys.Add(newFarm.Key);
        await App.Repo.UsersDb.UpdateItemAsync(App.CurrentUser);

        await DisplayAlert("Farm Added", $"Farm Name: {this.FarmName}\nFarm ID: {this.FarmId}", "OK");
        await Navigation.PopAsync();
    }
}