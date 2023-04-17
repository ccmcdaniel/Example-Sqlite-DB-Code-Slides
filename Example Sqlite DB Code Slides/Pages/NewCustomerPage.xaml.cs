namespace Example_Sqlite_DB_Code_Slides.Pages;

public partial class NewCustomerPage : ContentPage
{
	public NewCustomerPage()
	{
		InitializeComponent();
		PopulateStates();

	}



	private void PopulateStates()
	{
		List<string> states = new List<string>();
		states.Add("Select a State");
		states.Add("Alaska");
		states.Add("Alabama");
		states.Add("Arkansas");
		states.Add("American Samoa");
		states.Add("Arizona");
		states.Add("California");
		states.Add("Colorado");
		states.Add("Connecticut");
		states.Add("District of Columbia");
		states.Add("Delaware");
		states.Add("Florida");
		states.Add("Georgia");
		states.Add("Guam");
		states.Add("Hawaii");
		states.Add("Iowa");
		states.Add("Idaho");
		states.Add("Illinois");
		states.Add("Indiana");
		states.Add("Kansas");
		states.Add("Kentucky");
		states.Add("Louisiana");
		states.Add("Massachusetts");
		states.Add("Maryland");
		states.Add("Maine");
		states.Add("Michigan");
		states.Add("Minnesota");
		states.Add("Missouri");
		states.Add("Mississippi");
		states.Add("Montana");
		states.Add("North Carolina");
		states.Add("North Dakota");
		states.Add("Nebraska");
		states.Add("New Hampshire");
		states.Add("New Jersey");
		states.Add("New Mexico");
		states.Add("Nevada");
		states.Add("New York");
		states.Add("Ohio");
		states.Add("Oklahoma");
		states.Add("Oregon");
		states.Add("Pennsylvania");
		states.Add("Puerto Rico");
		states.Add("Rhode Island");
		states.Add("South Carolina");
		states.Add("South Dakota");
		states.Add("Tennessee");
		states.Add("Texas");
		states.Add("Utah");
		states.Add("Virginia");
		states.Add("Virgin Islands");
		states.Add("Vermont");
		states.Add("Washington");
		states.Add("Wisconsin");
		states.Add("West Virginia");
		states.Add("Wyoming");

		pckState.ItemsSource = states;
		pckState.SelectedIndex = 0;

	}

	private async void SubmitCustomerInfo(object sender, EventArgs e)
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

			Customer c = new Customer();
			c.CFName = txtFName.Text;
			c.CLName = txtLName.Text;
			c.CAddress1 = txtAddress1.Text;
			c.CAddress2 = txtAddress2.Text;
			c.CCity = txtCity.Text;
			c.CState = pckState.SelectedItem.ToString();
			c.CZip = txtZip.Text;
			await Database.SaveCustomerAsync(c);

			await Navigation.PopAsync();
		}
	}

	private bool CheckFormFilled()
	{
		bool result = true;

		if(txtFName.Text == null || txtFName.Text == String.Empty)
		{
			result = false;
			txtFName.BackgroundColor = Color.Parse("#FFAAAA");
		}
		else
			txtFName.BackgroundColor = Color.Parse("#FFFFFF");


		if (txtLName.Text == null || txtLName.Text == String.Empty)
		{
			result = false;
			txtLName.BackgroundColor = Color.Parse("#FFAAAA");
		}
		else
			txtLName.BackgroundColor = Color.Parse("#FFFFFF");


		if (txtAddress1.Text == null || txtAddress1.Text == String.Empty)
		{
			result = false;
			txtAddress1.BackgroundColor = Color.Parse("#FFAAAA");
		}
		else
			txtAddress1.BackgroundColor = Color.Parse("#FFFFFF");


		if (txtCity.Text == null || txtCity.Text == String.Empty)
		{
			result = false;
			txtCity.BackgroundColor = Color.Parse("#FFAAAA");
		}
		else
			txtCity.BackgroundColor = Color.Parse("#FFFFFF");

		if (pckState.SelectedIndex == 0)
		{
			result = false;
			pckState.BackgroundColor = Color.Parse("#FFAAAA");
		}
		else
			pckState.BackgroundColor = Color.Parse("#FFFFFF");

		if (txtZip.Text == null || txtZip.Text == String.Empty)
		{
			result = false;
			txtZip.BackgroundColor = Color.Parse("#FFAAAA");
		}
		else
			txtZip.BackgroundColor = Color.Parse("#FFFFFF");

		return result;

	}
}