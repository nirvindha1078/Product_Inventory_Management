using Product_Inventory_Management_API.Models;
using Product_Inventory_Management_API.Repositories;
using Product_Inventory_Management_API.Services;
using System.Collections.Generic;

namespace Product_Inventory_Management_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public void CreateProduct(Product product)
        {
            _productRepository.AddProduct(product);
        }

        public void UpdateProduct(int id, Product product)
        {
            _productRepository.UpdateProduct(id, product);
        }

        public void DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);
        }
    }
}
