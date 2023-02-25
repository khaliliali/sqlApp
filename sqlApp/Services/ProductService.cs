using Microsoft.Extensions.Configuration;
using sqlApp.Models;
using System.Data.SqlClient;
using System.Text.Json;

namespace sqlApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("SQLConnection"));
        }

        public async Task <List<Product>> GetProducts()
        {
            // Using AZ-Function
            String FunctionURL = "https://homorofuncapp.azurewebsites.net/api/GetProducts?code=bk1djJA2C87GQ0l2x4dbr8fVkuwFWhqpTPzyV4utj7LoAzFuJkYF6g==";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage rep = await client.GetAsync(FunctionURL);
                string content = await rep.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Product>>(content);
            }


            // Old verion

            //List<Product> _product_list = new List<Product>();
            //string _statement = "SELECT ProductID,ProductName,Quantity from Products";
            //SqlConnection _connection = GetConnection();

            //_connection.Open();

            //SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            //using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            //{
            //    while (_reader.Read())
            //    {
            //        Product _product = new Product()
            //        {
            //            ProductID = _reader.GetInt32(0),
            //            ProductName = _reader.GetString(1),
            //            Quantity = _reader.GetInt32(2)
            //        };

            //        _product_list.Add(_product);
            //    }
            //}
            //_connection.Close();
            //return _product_list;
        }

    }
}
