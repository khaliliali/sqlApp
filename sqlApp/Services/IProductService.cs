using sqlApp.Models;

namespace sqlApp.Services
{
    public interface IProductService
    {
        // Using AZ-Function
        Task<List<Product>> GetProducts();

        // Old Version
        //List<Product> GetProducts();
    }
}