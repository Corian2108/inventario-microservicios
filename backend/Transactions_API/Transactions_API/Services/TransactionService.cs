using Transactions_API.Entities;
using Transactions_API.Repositories;
using System.Net.Http.Json;

namespace Transactions_API.Services
{
    public class TransactionService
    {

        private readonly TransactionRepository _transactionRepository;
        private readonly HttpClient _httpClient;

        public TransactionService(
           TransactionRepository transactionRepository,
           IHttpClientFactory httpClientFactory)
        {
            _transactionRepository = transactionRepository;
            _httpClient = httpClientFactory.CreateClient("ProductsApi");
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _transactionRepository.GetByIdAsync(id);
        }

        public async Task<List<Transaction>> GetByFiltersAsync(
            DateTime? startDate,
            DateTime? endDate,
            int? type)
        {
            return await _transactionRepository.GetByFiltersAsync(startDate, endDate, type);
        }

        public async Task<bool> CreatePurchaseAsync(
            int productId,
            int quantity,
            DateTime transactionDate,
            string? detail,
            decimal unitPrice,
            decimal totalPrice)
        {
            if (quantity <= 0)
                throw new Exception("La cantidad debe ser mayor a 0");

            // Sumar stock
            var response = await _httpClient.PatchAsJsonAsync(
                $"/api/Products/{productId}/stock/buy",quantity);

            if (!response.IsSuccessStatusCode)
                throw new Exception("La compra no pudo ser procesada.");

            var transaction = new Transaction
            {
                ProductId = productId,
                Type = 1,
                Quantity = quantity,
                TransactionDate = transactionDate,
                Detail = detail,
                UnitPrice = unitPrice,
                TotalPrice = totalPrice
            };

            await _transactionRepository.CreateAsync(transaction);
            return true;
        }

        public async Task<bool> CreateSaleAsync(
            int productId,
            int quantity,
            DateTime transactionDate,
            string? detail,
            decimal unitPrice,
            decimal totalPrice
            )
        {
            if (quantity <= 0)
                throw new Exception("La cantidad debe ser mayor a cero."); ;

            // Restar stock
            var response = await _httpClient.PatchAsJsonAsync(
                $"/api/Products/{productId}/stock/sell",quantity);

            if (!response.IsSuccessStatusCode)
                throw new Exception("El stock es insuficiente.");

            var transaction = new Transaction
            {
                ProductId = productId,
                Type = 2,
                Quantity = quantity,
                TransactionDate = transactionDate,
                Detail = detail,
                UnitPrice = unitPrice,
                TotalPrice = totalPrice
            };

            await _transactionRepository.CreateAsync(transaction);
            return true;
        }

    }
}
