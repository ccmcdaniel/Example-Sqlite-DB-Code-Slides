using SQLite;

namespace Example_Sqlite_DB_Code_Slides
{
    public static class Database
    {
        public const string connFilename = "InvoiceDB.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the conn in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the conn if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded conn access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static SQLite.SQLiteAsyncConnection conn;

        public static async Task Init()
        {
            if (conn is not null)
                return;

            string connPath = Path.Combine(FileSystem.AppDataDirectory, connFilename);

            conn = new SQLiteAsyncConnection(connPath, Flags);

            _ = await conn.CreateTableAsync<Customer>();
            _ = await conn.CreateTableAsync<Order>();
        }

        public static async Task<List<Customer>> GetAllCustomersAsync()
        {
            await Init();
            return await conn.Table<Customer>().ToListAsync();
        }

        public static async Task<List<Customer>> GetAllOrdersAsync()
        {
            await Init();
            return await conn.Table<Customer>().ToListAsync();
        }

        public static async Task<int> SaveCustomerAsync(Customer item)
        {
            await Init();
            if (item.Id != 0)
                return await conn.UpdateAsync(item);
            else
                return await conn.InsertAsync(item);
        }

        public static async Task<int> SaveOrderAsync(Order item)
        {
            await Init();
            if (item.Id != 0)
                return await conn.UpdateAsync(item);
            else
                return await conn.InsertAsync(item);
        }

        public static async Task<List<Customer>> GetCustomersByState(string state_name)
        {
            await Init();
            return await conn.Table<Customer>().Where(
                customer => customer.CState == state_name
                ).ToListAsync();
            //return await conn.QueryAsync<Customer>($"SELECT * FROM [Customer] WHERE [CState] == {state_name}");
        }

        public static async Task<List<Order>> GetOrdersByCustomer(int customer_id)
        {
            await Init();
            return await conn.Table<Order>().Where(
                order => order.CustomerID == customer_id
                ).ToListAsync();
            //return await conn.QueryAsync<Customer>($"SELECT * FROM [Customer] WHERE [CState] == {state_name}");
        }

        public static async Task<List<Customer>> SearchForCustomer(string search_item)
       {
            await Init();
            
            return await conn.Table<Customer>().Where(
                    customer =>
                        (customer.CFName.ToLower().Contains(search_item.ToLower()) ||
                         customer.CLName.ToLower().Contains(search_item.ToLower()) ||
                         customer.CAddress1.ToLower().Contains(search_item.ToLower()) ||
                         customer.CAddress2.ToLower().Contains(search_item.ToLower()) ||
                         customer.CCity.ToLower().Contains(search_item.ToLower()) ||
                         customer.CState.ToLower().Contains(search_item.ToLower()) ||
                         customer.CZip.ToLower().Contains(search_item.ToLower())
                         )
                 ).ToListAsync();
        }
    }

    [Table("Customer")]
    public class Customer
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int Id { get; set; }
        public string CFName { get; set; }
        public string CLName { get; set; }
        public string CAddress1 { get; set; }
        public string CAddress2 { get; set; }
        public string CCity { get; set; }
        public string CState { get; set; }
        public string CZip { get; set; }
    }

    [Table("Order")]
    public class Order
    {
        [PrimaryKey, AutoIncrement, Column("_Id")]
        public int Id { get; set; }

        public int CustomerID { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal TotalCost { get; set; }
    }

}
