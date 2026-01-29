using Microsoft.AspNetCore.Mvc;
using Products_API.Entities;
using Products_API.Services;

namespace Products_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        // POST: api/products
        // Permite crear nuevos productos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            var id = await _productService.CreateAsync(product);
            return Ok(id);
        }

        // GET: api/products
        //Devuelve todos los productos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }


        // GET: api/categories
        //Devuelve todas las categorías
        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCat()
        {
            var category = await _productService.GetAllCatAsyn();
            return Ok(category);
        }

        // GET: api/products/{id}
        //Devuelve un producto consultado por id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }

        // PUT: api/products/{id}
        //Permite actualizar un producto por id incluye la capacidad de hacer eliminación lógica
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            await _productService.UpdateAsync(id, product);
            return NoContent();
        }

        // DELETE: api/products/{id}
        //Permite eliminar productos existentes de forma física
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }

        // PATCH: api/products/{id}/stock/sell
        //Permite registrar la venta de productos
        [HttpPatch("{id:int}/stock/sell")]
        public async Task<IActionResult> UpdateStockSell(int id, [FromBody] int stock)
        {
            await _productService.UpdateAsyncSell(id, stock);
            return NoContent();
        }

        // PATCH: api/products/{id}/stock/buy
        // Permite registrar compras de un producto existente
        [HttpPatch("{id:int}/stock/buy")]
        public async Task<IActionResult> UpdateStockBuy(int id, [FromBody] int stock)
        {
            await _productService.UpdateAsyncBuy(id, stock);
            return NoContent();
        }
    }
}
