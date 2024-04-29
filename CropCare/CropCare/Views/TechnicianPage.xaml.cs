using CropCare.Models;

namespace CropCare.Views;

/// <summary>
/// Represents a page for managing technicians.
/// </summary>
public partial class TechnicianPage : ContentPage
{
    /// <summary>
    /// Gets or sets the list of assigned technicians.
    /// </summary>
    public List<User> AssignedTechnicians { get; set; }

    /// <summary>
    /// Gets or sets the list of all technicians.
    /// </summary>
    public List<User> Technicians { get; set; }

    /// <summary>
    /// Gets the farm associated with the page.
    /// </summary>
    public Farm Farm { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TechnicianPage"/> class.
    /// </summary>
    /// <param name="farm">The farm associated with the page.</param>
    public TechnicianPage(Farm farm)
	{
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
        //Retrieve all Technicians
        this.Technicians = new List<User>();
        this.Technicians.AddRange((await App.Repo.UsersDb.GetItemsAsync(true)).Where(u => u.IsOwner == false));

        //Check users that are attachted to this farm
        var UserKeys = App.Repo.UserToFarmDb.Items.Where(u => u.FarmId == this.Farm.Key).Select(u => u.UserId).ToList();
        this.AssignedTechnicians = App.Repo.UsersDb.Items.Where(u => UserKeys.Contains(u.Key) && u.IsOwner == false).ToList();
        this.Technicians.ForEach(u => u.IsAssigned = this.AssignedTechnicians.Any(t => t.Key == u.Key));
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
}