using Microsoft.AspNetCore.Mvc.Formatters;
using System.Data.SqlClient;
using TozartSqlApp.Models;

namespace TozartSqlApp.Services
{
    public class ProductService
    {
        private static string db_source = "tozartserver.database.windows.net";
        private static string db_user = "sqlAdmin";
        private static string db_password = "9781Adagui#$%";
        private static string db_database = "tozartdb";


        private SqlConnection GetConnection() 
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Product> GetProducts() 
        {
            SqlConnection conn = this.GetConnection();
            List<Product> products = new List<Product>();
            string statement = "SELECT ProductID, ProductName, Quantity FROM Products";
            conn.Open();
            SqlCommand cmd = new SqlCommand(statement, conn);
            using (SqlDataReader reader = cmd.ExecuteReader()) 
            {
                while (reader.Read()) 
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    products.Add(product);
                }
            }
            conn.Close();
            return products;
        }

    }
}
