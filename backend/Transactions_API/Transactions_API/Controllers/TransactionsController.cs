using Microsoft.AspNetCore.Mvc;
using Transactions_API.Services;
using Transactions_API.Entities;

namespace Transactions_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {

        private readonly TransactionService _transactionService;

        public TransactionsController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/transactions
        // Devuelve las transacciones por filtros dinámicos
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate,
            [FromQuery] int? type)
        {
            var transactions = await _transactionService
                .GetByFiltersAsync(startDate, endDate, type);

            return Ok(transactions);
        }

        // GET: api/transactions/{id}
        // Devuelve las productos filtradas por id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        // POST: api/transactions/purchase
        // Registra las transacciones que agregan stock
        [HttpPost("purchase")]
        public async Task<IActionResult> CreatePurchase(
            [FromBody] Transaction transaction)
        {
            var result = await _transactionService.CreatePurchaseAsync(
                                    transaction.ProductId,
                                    transaction.Quantity,
                                    transaction.TransactionDate,
                                    transaction.Detail,
                                    transaction.UnitPrice,
                                    transaction.TotalPrice
                                );

            if (!result)
                return BadRequest("La compra no pudo ser procesada.");

            return Created(string.Empty, null);
        }

        // POST: api/transactions/sale
        // Registra las transacciones de ventas
        [HttpPost("sale")]
        public async Task<IActionResult> CreateSale(
             [FromBody] Transaction transaction)
        {
            var result = await _transactionService.CreateSaleAsync(
                                    transaction.ProductId,
                                    transaction.Quantity,
                                    transaction.TransactionDate,
                                    transaction.Detail,
                                    transaction.UnitPrice,
                                    transaction.TotalPrice
                                );

            if (!result)
                return Conflict("La venta no pudo ser procesada, stock insuficiente.");

            return Created(string.Empty, null);
        }

    }
}
