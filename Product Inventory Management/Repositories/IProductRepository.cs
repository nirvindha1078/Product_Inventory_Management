using Product_Inventory_Management_API.Models;
using System.Collections.Generic;

namespace Product_Inventory_Management_API.Repositories
{
    
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(int id, Product product);
        void DeleteProduct(int id);
    }
    
}
