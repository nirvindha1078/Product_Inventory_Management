using Product_Inventory_Management_API.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Product_Inventory_Management_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _products;

        public ProductRepository()
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "products.json");
            _products = LoadProductsFromFile(jsonFilePath);
        }

        private List<Product> LoadProductsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} does not exist.");
            }

            var jsonString = File.ReadAllText(filePath);
            var products = JsonSerializer.Deserialize<List<Product>>(jsonString);

            if (products == null)
            {
                return new List<Product>();  
            }
            else
            {
                return products;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.ProductId == id);
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
            SaveProductsToFile();
        }

        public void UpdateProduct(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.ProductId == id);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductBrand = product.ProductBrand;
                existingProduct.ProductReleaseYear = product.ProductReleaseYear;
                existingProduct.ProductPrice = product.ProductPrice;

                SaveProductsToFile();
            }
        }

        public void DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                _products.Remove(product);
                SaveProductsToFile();
            }
        }

        private void SaveProductsToFile()
        {
            var jsonString = JsonSerializer.Serialize(_products, new JsonSerializerOptions { WriteIndented = true });
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "products.json");
            File.WriteAllText(jsonFilePath, jsonString);
        }
    }
}
