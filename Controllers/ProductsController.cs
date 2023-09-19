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
            var products = _context.Products.ToList();
            if(products is null)
            {
                return NotFound("Products  not found!");
            }
            return products;    
        }

        // fazendo a busca somente de um producto
        [HttpGet("{id:int}", Name = "GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            var product = _context.Products.FirstOrDefault(prod => prod.ProductId == id);
            if(product is null)
            {
                return NotFound("Product not found!");
            }

            return product;
        }

        // método para criação de um produto
        [HttpPost]
        public ActionResult Post(Product product)
        {
            if(product is null)
            {
                return BadRequest();
            }
            _context.Products.Add(product);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetProduct",
                new { id = product.ProductId }, product);
        }

        // método para atualização do produto
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if(id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(product);
        }
    }
}
