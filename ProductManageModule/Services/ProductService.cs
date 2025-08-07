using Microsoft.AspNetCore.Http;
using ProductManagementModule.Domain;
using ProductManagementModule.Utils;

using System.Data;
using Pomelo.EntityFrameworkCore.MySql; // nếu bạn dùng MySQL thì dùng MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using ProductManagementModule.IRepositories;

namespace ProductManagementModule.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository repository)
        {
            _productRepository = repository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public Product GetProductById(long id)
        {
            return _productRepository.GetProductById(id);
        }

        public void AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
        }

        public void DeleteProduct(long id)
        {
            _productRepository.DeleteProduct(id);
        }

        public PaginatedProducts GetPaginatedProducts(int page, int pageSize)
        {
            var allProducts = _productRepository.GetAllProducts();
            var productCount = allProducts.Count();
            var products = allProducts.Skip((page - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToList();
            var totalPages = (int)Math.Ceiling((double)productCount / pageSize);
            return new PaginatedProducts
            {
                Products = products,
                CurrentPage = page,
                TotalPages = totalPages
            };
        }

        public async Task<string?> SaveProductImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }

            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            var fileExtension = Path.GetExtension(imageFile.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadDir, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "/images/" + uniqueFileName;
        }

        public async Task<bool> UpdateProductAsync(Product updatedProduct, IFormFile imageFile)
        {
            var existingProduct = _productRepository.GetProductById(updatedProduct.Id);
            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.ProductName = updatedProduct.ProductName;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Quantity = updatedProduct.Quantity;
            existingProduct.Unit = updatedProduct.Unit;
            existingProduct.Description = updatedProduct.Description;

            if (imageFile != null && imageFile.Length > 0)
            {
                existingProduct.ImageUrl = await SaveProductImageAsync(imageFile);
            }

            _productRepository.UpdateProduct(existingProduct);
            return true;
        }
    }
}
