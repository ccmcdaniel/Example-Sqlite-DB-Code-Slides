namespace Example_Sqlite_DB_Code_Slides.Pages;

public partial class NewOrderPage : ContentPage
{
	int customerID;

	public NewOrderPage(int customerID)
	{
		this.customerID = customerID;
		InitializeComponent();
	}



	private bool CheckFormFilled()
	{
		bool result = true;

		if (txtItemName.Text == null || txtItemName.Text == String.Empty)
		{
			result = false;
			txtItemName.BackgroundColor = Color.Parse("#FFAAAA");
		}
		else
			txtItemName.BackgroundColor = Color.Parse("#FFFFFF");


		if (txtColor.Text == null || txtColor.Text == String.Empty)
		{
			result = false;
			txtColor.BackgroundColor = Color.Parse("#FFAAAA");
		}
		else
			txtColor.BackgroundColor = Color.Parse("#FFFFFF");


		decimal test;
		if (txtAmount.Text == null || txtAmount.Text == String.Empty || Decimal.TryParse(txtAmount.Text, out test) == false)
		{
			result = false;
			txtAmount.BackgroundColor = Color.Parse("#FFAAAA");
		}
		else
			txtAmount.BackgroundColor = Color.Parse("#FFFFFF");

   

		return result;

	}

	private async void SubmitOrder(object sender, EventArgs e)
	{
		if (CheckFormFilled() == false)
		{
			lblError.IsVisible = true;
			return;
		}
		else
		{
			//Submit Customer Info to Database.
			lblError.IsVisible = false;

			Order order = new Order();
			order.Item = txtItemName.Text;
			order.Quantity = Convert.ToInt32(sliderQuantity.Value);
			order.Color = txtColor.Text;
			order.TotalCost = Decimal.Parse(txtAmount.Text);
			order.CustomerID = customerID;

			await Database.SaveOrderAsync(order);

			await Navigation.PopAsync();
		}
	}
}