using System.Collections.ObjectModel;

namespace Example_Sqlite_DB_Code_Slides.Pages;

public partial class CustomerOrdersPage : ContentPage
{
	Customer customer;

	public CustomerOrdersPage(Customer c)
	{
		InitializeComponent();
		customer = c;
		lblCustomerName.Text = $"{c.CLName}, {c.CFName}";

		MainThread.BeginInvokeOnMainThread(async () =>
		{
			await RefreshTable();
		});
	}

	public async Task<int> RefreshTable()
	{
		var customer_orders = await Database.GetOrdersByCustomer(customer.Id);

		ObservableCollection<Order> orders = new ObservableCollection<Order>(customer_orders);
		collectionCustomerOrders.ItemsSource = orders;
	 
		return 1;
	}

	private async void RefreshTableEvent(object sender, EventArgs e)
	{
		await RefreshTable();
	}

	private void FilterCustomers(object sender, TextChangedEventArgs e)
	{

	}

	private void ToggleCommands(object sender, SelectionChangedEventArgs e)
	{
		if (collectionCustomerOrders.SelectedItem != null)
			layoutOrderCommands.IsVisible = true;
		else
			layoutOrderCommands.IsVisible = false;

	}

	private void EditOrder(object sender, EventArgs e)
	{

	}

	private void ShowAddNewOrderForm(object sender, EventArgs e)
	{
		var page = new Pages.NewOrderPage(customer.Id);

		page.Disappearing += RefreshTableEvent;

		Navigation.PushAsync(page);
	}
}