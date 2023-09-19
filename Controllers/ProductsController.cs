using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;

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

        // metodo get para buscar os dados no banco
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _context.Products.ToList();
            if(products is null)
            {
                return NotFound("Products  not found!");
            }
            return products;    
        }

        // fazendo a busca somente de um producto
        [HttpGet("{id:int}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _context.Products.FirstOrDefault(prod => prod.ProductId == id);
            if(product is null)
            {
                return NotFound("Product not found!");
            }

            return product;
        }
    }
}
