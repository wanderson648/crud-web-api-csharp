using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // método get para buscar os dados no banco
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                var products = _context.Products.Take(10).AsNoTracking().ToList();
                if (products is null)
                {
                    return NotFound("Products  not found!");
                }
                return products;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while processing your request");
            }
        }

        // fazendo a busca somente de um producto
        [HttpGet("{id:int}", Name = "GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(prod => prod.ProductId == id);
                if (product is null)
                {
                    return NotFound("Product not found!");
                }

                return product;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while processing your request");
            }
        }

        // método para criação de um produto
        [HttpPost]
        public ActionResult Post(Product product)
        {
            try
            {
                if (product is null)
                {
                    return BadRequest("Product cannot be null");
                }
                _context.Products.Add(product);
                _context.SaveChanges();

                return new CreatedAtRouteResult("GetProduct",
                    new { id = product.ProductId }, product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while processing your request");
            }
        }

        // método para atualização do produto
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            try
            {
                if (id != product.ProductId)
                {
                    return BadRequest($"Product {id} not exists");
                }

                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while processing your request");
            }
        }

        // deletando produto do banco
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(product => product.ProductId == id);

                if (product is null)
                {
                    return NotFound("Product not found!");
                }
                _context.Products.Remove(product);
                _context.SaveChanges();

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while processing your request");
            }
        }
    }
}
