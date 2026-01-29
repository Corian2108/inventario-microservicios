using Microsoft.EntityFrameworkCore;
using Products_API.Data;
using Products_API.Entities;

namespace Products_API.Repositories
{
    public class ProductRepository
    {
        private readonly ProductsDbContext _context;

        public ProductRepository(ProductsDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.ProductId;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
