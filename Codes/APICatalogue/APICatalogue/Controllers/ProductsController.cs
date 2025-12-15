using Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;

namespace APICatalogue.Controllers
{
    [Route("[controller]")]//products
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly IRepository<Product> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("products/{id}")]
        public ActionResult<IEnumerable<Product>> GetProductByCategory(int id)
        {
            var product = _unitOfWork.ProductRepository.GetProductsByCategory(id);

            if (product is null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _unitOfWork.ProductRepository.GetAll();

            if (products is null)
            {
                return NotFound("Products not found");
            }

            return Ok(products);
        }
        [HttpGet("{id:int:min(1)}", Name="GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            var product = _unitOfWork.ProductRepository.Get(c => c.Id == id);

            if (product is null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product is null)
                return BadRequest();

            _unitOfWork.ProductRepository.Create(product);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();

            return Ok(product);

        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult Delete(int id)
        {
            var product = _unitOfWork.ProductRepository.Get(c => c.Id == id);

            if (product is null)
            {
                return NotFound("Category not found");
            }

            var deletedProduct = _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Commit();

            return Ok(deletedProduct);
        }
    }
}
