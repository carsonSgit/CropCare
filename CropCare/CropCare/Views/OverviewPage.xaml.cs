using CommunityToolkit.Maui.Core.Extensions;
using CropCare.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CropCare.Views;

public partial class OverviewPage : ContentPage
{

    public ObservableCollection<Farm> FarmsCollection { get; set; }
    public bool IsOwner { get; set; } = App.CurrentUser.IsOwner;

    public OverviewPage()
    {
        InitializeComponent();
        BindingContext = this;
        UpdateFarmCollectionList();
    }


    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        UpdateFarmCollectionList();
        IsOwner = App.CurrentUser.IsOwner;
    }

    private void UpdateFarmCollectionList()
    {
        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            DisplayAlert("No Internet", "Please check your internet connection", "OK");
            return;
        }
        try
        {
            var FarmKeys = App.Repo.UserToFarmDb.Items.Where(u => u.UserId == App.CurrentUser.Key).Select(u => u.FarmId).ToList();
            FarmsCollection = App.Repo.FarmsDb.Items.Where(f => FarmKeys.Contains(f.Key)).ToObservableCollection();
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "Could not fetch farms", "OK");
        }
    }

    private async void Btn_Add_Farm(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddFarmPage(UpdateFarmCollectionList));
    }

    private async void Farm_Tapped(object sender, TappedEventArgs e)
    {
        var farm = (sender as Frame)?.BindingContext as Farm;
        await Navigation.PushAsync(new DashboardPage(farm));
    }

}