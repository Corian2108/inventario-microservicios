using Microsoft.EntityFrameworkCore;
using Transactions_API.Entities;

namespace Transactions_API.Data
{
    public class TransactionsDbContext: DbContext
    {
        public TransactionsDbContext(DbContextOptions<TransactionsDbContext> options) 
            : base(options) 
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
