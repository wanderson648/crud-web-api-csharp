using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // buscado todos os produtos e categorieas
        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
        {
            return _context.Categories.Include(product => product.Products)
                .Where(category => category.CategoryId <= 5).ToList();
        }

        // buscando todas as categorias
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            return _context.Categories.ToList();
        }

        // buscando todas as categorias por id
        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> Get(int id)
        {
            var category = _context.Categories.FirstOrDefault(category => category.CategoryId == id);
            if (category is null)
            {
                return NotFound("Category not found!");
            }
            return Ok(category);
        }

        // cadastrar uma categoria
        [HttpPost]
        public ActionResult Post(Category category)
        {
            if (category is null)
            {
                return BadRequest();
            }
            _context.Categories.Add(category);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetCategory",
                new { id = category.CategoryId }, category);
        }

        // atualizar categoria
        [HttpPut("{id:int}")]
        public ActionResult<Category> Put(int id, Category category)
        {
            if(id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(category);
        }


        // deletar uma categoria
        [HttpDelete("{id:int}")]
        public ActionResult<Category> Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(category => category.CategoryId == id);
            if(category is null)
            {
                return NotFound("Category not Found!");
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok(category);
        }

    }
}
