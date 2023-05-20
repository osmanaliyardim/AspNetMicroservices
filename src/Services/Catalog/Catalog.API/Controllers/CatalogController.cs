using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();

            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = nameof(GetProductById))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductById(string id)
        {
            var product = await _productRepository.GetProductById(id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("{categoryName}", Name = nameof(GetProductByCategory))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string categoryName)
        {
            var products = await _productRepository.GetProductByCategory(categoryName);

            return Ok(products);
        }

        [Authorize("ClientIdPolicy")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productRepository.CreateProduct(product);

            return CreatedAtRoute(nameof(GetProductById), new { id = product.Id }, product);
        }

        [Authorize("ClientIdPolicy")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        public async Task<IActionResult> UpdatePoduct([FromBody] Product product)
        {
            return Ok(await _productRepository.UpdateProduct(product));
        }

        [Authorize("ClientIdPolicy")]
        [HttpDelete("{id:length(24)}", Name = nameof(DeleteProduct))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await _productRepository.DeleteProduct(id));
        }
    }
}
