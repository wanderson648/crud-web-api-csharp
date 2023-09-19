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
            try
            {
                return _context.Categories.Include(product => product.Products)
               .Where(category => category.CategoryId <= 5).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while processing your request");
            }
        }

        // buscando todas as categorias
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                return _context.Categories.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while processing your request");
            }
        }

        // buscando todas as categorias por id
        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> Get(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(category => category.CategoryId == id);
                if (category is null)
                {
                    return NotFound($"Category {id} not found!");
                }
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while processing your request");
            }
        }

        // cadastrar uma categoria
        [HttpPost]
        public ActionResult Post(Category category)
        {
            try
            {
                if (category is null)
                {
                    return BadRequest("Category cannot be null");
                }
                _context.Categories.Add(category);
                _context.SaveChanges();

                return new CreatedAtRouteResult("GetCategory",
                    new { id = category.CategoryId }, category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while processing your request");
            }
        }

        // atualizar categoria
        [HttpPut("{id:int}")]
        public ActionResult<Category> Put(int id, Category category)
        {
            try
            {
                if (id != category.CategoryId)
                {
                    return BadRequest($"Category {id} not exists");
                }

                _context.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while processing your request");
            }
        }


        // deletar uma categoria
        [HttpDelete("{id:int}")]
        public ActionResult<Category> Delete(int id)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(category => category.CategoryId == id);
                if (category is null)
                {
                    return NotFound("Category not Found!");
                }
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error ocurred while processing your request");
            }
        }

    }
}
