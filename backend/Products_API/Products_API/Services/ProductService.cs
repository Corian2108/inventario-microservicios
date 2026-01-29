using Products_API.Entities;
using Products_API.Repositories;

namespace Products_API.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            if (product.Price < 0)
                throw new ArgumentException("El precio tiene que ser mayor a 0.");

            if (product.Stock < 0)
                throw new ArgumentException("El stock minimo tiene que ser mayor a 0.");

            await _productRepository.AddAsync(product);
            return product;
        }

        public async Task<bool> UpdateAsync(int id, Product product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return false;

            if (product.Price <= 0 || product.Stock <= 0)
                throw new ArgumentException("Precio y stock no pueden ser menores o igual a 0");

            existingProduct.Name = product.Name;
            existingProduct.CategoryFk = product.CategoryFk;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.Batch = product.Batch;
            existingProduct.EntryDate = product.EntryDate;
            existingProduct.ImageUrl = product.ImageUrl;

            await _productRepository.UpdateAsync(existingProduct);
            return true;
        }

        public async Task<bool> UpdateAsyncSell (int id, int stock)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return false;

            if (existingProduct.Stock < stock )
                throw new ArgumentException("No hay stock suficiente para la venta");

            int newStock = existingProduct.Stock - stock;

            existingProduct.Stock = newStock;

            await _productRepository.UpdateAsync(existingProduct);
            return true;
        }

        public async Task<bool> UpdateAsyncBuy(int id, int stock)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                return false;

            int newStock = existingProduct.Stock + stock;

            existingProduct.Stock = newStock;

            await _productRepository.UpdateAsync(existingProduct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return false;

            await _productRepository.DeleteAsync(product);
            return true;
        }
    }
}
