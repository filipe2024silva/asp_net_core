using Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Models;

namespace APICatalogue.Controllers
{
    [Route("[controller]")]//products
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await _context.Products!.AsNoTracking().ToListAsync(); 
        }

        [HttpGet("{id:int:min(1)}", Name="GetProduct")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _context.Products?.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product is Product)
                return BadRequest();

            _context.Products.Add(product);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(product);

        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            var product = _context.Products?.FirstOrDefault(p => p.Id == id);
            
            if (product is null)
            {
                return NotFound("Product not found");
            }
            _context.Products?.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }
    }
}
