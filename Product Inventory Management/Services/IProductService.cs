using Product_Inventory_Management_API.Models;
using System.Collections.Generic;

namespace Product_Inventory_Management_API.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void CreateProduct(Product product);
        void UpdateProduct(int id, Product product);
        void DeleteProduct(int id);
    }
}
