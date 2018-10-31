using System.Collections.Generic;
using SampleAspNet.Models;
using MySql.Data.MySqlClient;

namespace SampleAspNet
{
    public class ProductRepository
    {
        public static string ConnectionString { get; set; }

        public List<Product> GetAllProducts()
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            List<Product> products = new List<Product>();

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ProductID, Name, Price, CategoryID FROM products;";

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Product product = new Product()
                    {
                        Id = (int)dataReader["ProductID"],
                        Name = dataReader["Name"].ToString(),
                        Price = (decimal)dataReader["Price"],
                        CategoryId = (int)dataReader["CategoryID"]
                    };

                    products.Add(product);
                }

                return products;
            }
        }

        public int InsertProduct(string name, decimal price, int categoryId)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "INSERT INTO Products (Name, Price, CategoryID) " +
                                  "VALUES (@name, @price, @categoryid);";
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("price", price);
                cmd.Parameters.AddWithValue("categoryid", categoryId);

                return cmd.ExecuteNonQuery();
            }
        }

        public int UpdateProduct(Product product)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "UPDATE Products SET Name = @name, Price = @price " +
                                  "WHERE ProductID = @pid";
                cmd.Parameters.AddWithValue("name", product.Name);
                cmd.Parameters.AddWithValue("price", product.Price);
                cmd.Parameters.AddWithValue("pid", product.Id);

                return cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int id)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM Products " +
                                                    "WHERE ProductID=" + id, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Product> GetProductsByName(string Name)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            List<Product> products = new List<Product>();

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ProductID, Name, Price, CategoryID " +
                                  "FROM products " +
                                  "WHERE Name = @xyz " +
                                  "ORDER BY ProductID";
                cmd.Parameters.AddWithValue("xyz", Name);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    Product product = new Product()
                    {
                        Id = (int)dataReader["ProductID"],
                        Name = dataReader["Name"].ToString(),
                        Price = (decimal)dataReader["Price"],
                        CategoryId = (int)dataReader["CategoryID"]
                    };

                    products.Add(product);
                }

                return products;
            }
        }

        public Product GetProduct(int id)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ProductID, Name, Price, CategoryID " +
                                  "FROM products " +
                                  "WHERE ProductID=" + id;

                MySqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.Read())
                {
                    Product product = new Product()
                    {
                        Name = dataReader["Name"].ToString(),
                        Id = (int)dataReader["ProductID"],
                        Price = (decimal)dataReader["Price"],
                        CategoryId = (int)dataReader["CategoryID"]
                    };

                    return product;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
