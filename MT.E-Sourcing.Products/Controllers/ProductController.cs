using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MT.E_Sourcing.Core;
using MT.E_Sourcing.Data.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MT.E_Sourcing.Products.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        #region Variables
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;
        #endregion
        #region Constructor
        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _logger = logger;
            _productRepository = productRepository;
        }
        #endregion

        #region Crud Op

        [HttpGet]

        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();

            return Ok(products);
        }
        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                _logger.LogError($"Product with id : {id}, has not been found  in database");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productRepository.Add(product);
            return CreatedAtRoute("GetProduct", new {id=product.Id },product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]

        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _productRepository.Update(product));
        }
        #endregion
        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                _logger.LogError($"Product with id : {id}, has not been found  in database");
                return NotFound();
            }
            return Ok(await _productRepository.Delete(id));
        }
    }
}
