using Microsoft.EntityFrameworkCore;
using Transactions_API.Data;
using Transactions_API.Entities;

namespace Transactions_API.Repositories
{
    public class TransactionRepository
    {
        private readonly TransactionsDbContext _context;

        public TransactionRepository(TransactionsDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            return transaction.TransactionId;
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(p => p.TransactionId == id);

            if (transaction != null)
            {
                transaction.TypeName = transaction.Type == 1 ? "Compra" : "Venta";
            }

            return transaction;
        }

        public async Task<List<Transaction>> GetByFiltersAsync(
            DateTime? startDate,
            DateTime? endDate,
            int? type)
        {
            var query = _context.Transactions.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(t => t.TransactionDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.TransactionDate <= endDate.Value);

            if (type != null)
                query = query.Where(t => t.Type == type);

            var list = await query
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();

            // asignar el nombre legible del tipo
            foreach (var t in list)
            {
                t.TypeName = t.Type == 1 ? "Compra" : "Venta";
            }

            return list;
        }

    }
}
