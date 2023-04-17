using System.Collections.ObjectModel;

namespace Example_Sqlite_DB_Code_Slides;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

		MainThread.BeginInvokeOnMainThread(async () =>
		{
			await RefreshTable();
		});
	}

	private void ShowAddNewCustomerForm(object sender, EventArgs e)
	{
		var page = new Pages.NewCustomerPage();

		page.Disappearing += RefreshTableEvent;

		Navigation.PushAsync(page);
	}

	public async Task<int> RefreshTable()
	{
		if (txtFilter.Text == null || txtFilter.Text == String.Empty)
		{
			var customers = await Database.GetAllCustomersAsync();

			ObservableCollection<Customer> teamsObs = new ObservableCollection<Customer>(customers);
			collectionCustomers.ItemsSource = teamsObs;
		}
		else
		{
			var customers = await Database.SearchForCustomer(txtFilter.Text);

			ObservableCollection<Customer> teamsObs = new ObservableCollection<Customer>(customers);
			collectionCustomers.ItemsSource = teamsObs;
		}

		return 1;
	}

	private async void RefreshTableEvent(object sender, EventArgs e)
	{
		await RefreshTable();
	}

	private async void FilterCustomers(object sender, TextChangedEventArgs e)
	{
		await RefreshTable();
	}

	private void ToggleCommands(object sender, SelectionChangedEventArgs e)
	{
		if(collectionCustomers.SelectedItem!= null) 
			layoutCustomerCommands.IsVisible = true;
		else
			layoutCustomerCommands.IsVisible = false;
	}

	private void ShowOrders(object sender, EventArgs e)
	{
		Customer c = collectionCustomers.SelectedItem as Customer;

		
		var page = new Pages.CustomerOrdersPage(c);
		page.Disappearing += RefreshTableEvent;
		Navigation.PushAsync(page);
	}
}

