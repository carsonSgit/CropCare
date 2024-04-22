using CropCare.Models;
using System.Collections.ObjectModel;

namespace CropCare.Views;

public partial class OverviewPage : ContentPage
{
	public ObservableCollection<Farm> FarmsCollection { get; set; }

	public OverviewPage()
	{
        FarmsCollection = App.Repo.FarmsDb.Items;

		InitializeComponent();
		BindingContext = this;
	}

    private async void Btn_Add_Farm(object sender, EventArgs e)
    {
		Farm newFarm = new Farm("test");
		await App.Repo.FarmsDb.AddItemAsync(newFarm);
		App.CurrentUser.FarmKeys.Add(newFarm.Key);
		await App.Repo.UsersDb.UpdateItemAsync(App.CurrentUser);
    }
}