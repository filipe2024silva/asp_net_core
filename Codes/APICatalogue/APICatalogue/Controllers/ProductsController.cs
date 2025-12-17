using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Pagination;
using Repositories;
using X.PagedList;

namespace APICatalogue.Controllers
{
    [Route("[controller]")]//products
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //private readonly IRepository<Product> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("products/{id}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductByCategory(int id)
        {
            var products = await _unitOfWork.ProductRepository.GetProductsByCategoryAsync(id);

            if (products is null)
            {
                return NotFound("Product not found");
            }

            //var destiny = _mapper.Map<Destiny>(Origin);
            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productsDTO);
        }
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get([FromQuery] ProductsParameters productsParameters)
        {
            var products = await _unitOfWork.ProductRepository.GetProductsAsync(productsParameters);

            return GetProducts(products);
        }
        [HttpGet("filter/price/pagination")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsFilterPrice([FromQuery] ProductsFilterPrice productsFilterParams)
        {
            var products = await _unitOfWork.ProductRepository.GetProductsFilterPriceAsync(productsFilterParams);

            return GetProducts(products);
        }

        private ActionResult<IEnumerable<ProductDTO>> GetProducts(IPagedList<Product>? products)
        {
            var metadata = new
            {
                products.Count,
                products.PageSize,
                products.PageCount,
                products.TotalItemCount,
                products.HasNextPage,
                products.HasPreviousPage
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            if (products is null)
            {
                return NotFound("Products not found");
            }

            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productsDTO);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();

            if (products is null)
            {
                return NotFound("Products not found");
            }

            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productsDTO);
        }
        [HttpGet("{id:int:min(1)}", Name="GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(c => c.Id == id);

            if (product is null)
            {
                return NotFound("Product not found");
            }

            var productDTO = _mapper.Map<ProductDTO>(product);

            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post(ProductDTO productDTO)
        {
            if (productDTO is null)
                return BadRequest();

            var product = _mapper.Map<Product>(productDTO);

            _unitOfWork.ProductRepository.Create(product);
            await _unitOfWork.CommitAsync();

            var productDTOCreated = _mapper.Map<ProductDTO>(product);

            return new CreatedAtRouteResult("GetProduct", new { id = productDTOCreated.Id }, productDTOCreated);
        }

        [HttpPatch("{id}/updatePartial")]
        public async Task<ActionResult<ProductDTOUpdateResponse>> Patch(int id, JsonPatchDocument<ProductDTOUpdateRequest> patchProductDTO)
        {
            if (patchProductDTO is null || id <= 0)
            {
                return BadRequest();
            }

            var product = await _unitOfWork.ProductRepository.GetAsync(c => c.Id == id);

            if (product is null)
            {
                return NotFound("Product not found");
            }

            var productUpdateRequest = _mapper.Map<ProductDTOUpdateRequest>(product);

            patchProductDTO.ApplyTo(productUpdateRequest, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(productUpdateRequest))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(productUpdateRequest, product);

            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.CommitAsync();

            return Ok(_mapper.Map<ProductDTOUpdateResponse>(product));
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<ProductDTO>> Put(int id, ProductDTO productDTO)
        {
            if (id != productDTO.Id)
                return BadRequest();

            var product = _mapper.Map<Product>(productDTO);

            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.CommitAsync();

            var productDTOUpdated = _mapper.Map<ProductDTO>(product);

            return Ok(productDTOUpdated);

        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(c => c.Id == id);

            if (product is null)
            {
                return NotFound("Category not found");
            }

            var deletedProduct = _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.CommitAsync();

            var deletedProductDTO = _mapper.Map<ProductDTO>(deletedProduct);

            return Ok(deletedProductDTO);
        }
    }
}
