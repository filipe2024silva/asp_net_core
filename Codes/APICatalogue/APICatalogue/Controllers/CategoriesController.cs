using Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APICatalogue.Controllers
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

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
        {
            return _context.Categories.Include(p => p.Products).ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _context.Categories.AsNoTracking().ToList();
            if (categories is null)
            {
                return NotFound("Categories not found");
            }
            return categories;
        }


        [HttpGet("{id:int:min(1)}", Name = "GetCategory")]
        public ActionResult<Category> Get(int id)
        {
            var category = _context.Categories?.FirstOrDefault(p => p.Id == id);

            if (category is null)
            {
                return NotFound("Category not found");
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            if (category is Category)
                return BadRequest();

            _context.Categories.Add(category);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetCategory", new { id = category.Id }, category);
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.Id)
                return BadRequest();

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(category);
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            var category = _context.Categories?.FirstOrDefault(p => p.Id == id);

            if (category is null)
            {
                return NotFound("Category not found");
            }
            _context.Categories?.Remove(category);
            _context.SaveChanges();

            return Ok(category);
        }
    }
}
