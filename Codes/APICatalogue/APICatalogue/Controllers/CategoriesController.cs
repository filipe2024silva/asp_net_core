using Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;
using APICatalogue.DTOs.Mappings;
using DTOs;
using Newtonsoft.Json;
using Pagination;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace APICatalogue.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            if (categories is null)
            {
                return NotFound("Categories not found");
            }
            var categoryDTO = categories.ToCategoryDTOList();

            return Ok(categoryDTO);
        }
        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get([FromQuery] CategoriesParameters categoriesParameters)
        {
            var categories = await _unitOfWork.CategoryRepository.GetCategoriesAsync(categoriesParameters);

            return GetCategories(categories);
        }
        [HttpGet("filter/name/pagination")]
        public async  Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesFiltered([FromQuery] CategoriesFilterName categoriesFiltered)
        {
            var categories = await _unitOfWork.CategoryRepository.GetCategoriesFilterNameAsync(categoriesFiltered);

            return GetCategories(categories);
        }

        private ActionResult<IEnumerable<CategoryDTO>> GetCategories(IPagedList<Category> categories)
        {
            var metadata = new
            {
                categories.Count,
                categories.PageSize,
                categories.PageCount,
                categories.TotalItemCount,
                categories.HasNextPage,
                categories.HasPreviousPage
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var categoriesDTO = categories.ToCategoryDTOList();

            return Ok(categoriesDTO);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id);

            if (category is null)
            {
                return NotFound("Category not found");
            }
            var categoryDTO = category.ToCategoryDTO();

            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post(CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
                return BadRequest();

            var category = categoryDTO.ToCategory();

            var categoryCreated = _unitOfWork.CategoryRepository.Create(category);
            await _unitOfWork.CommitAsync();

            var categoryCreatedDTO = categoryCreated.ToCategoryDTO();

            return new CreatedAtRouteResult("GetCategory", new { id = categoryCreatedDTO.Id }, categoryCreatedDTO);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<CategoryDTO>> Put(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
                return BadRequest();

            var category = categoryDTO.ToCategory();

            var updatedCategory = _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.CommitAsync();

            var categoryUpdatedDTO = updatedCategory.ToCategoryDTO();

            return Ok(categoryUpdatedDTO);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id);

            if (category is null)
            {
                return NotFound("Category not found");
            }

            var deletedCategory = _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.CommitAsync();

            var deletedCategoryDTO = deletedCategory.ToCategoryDTO();

            return Ok(deletedCategoryDTO);
        }
    }
}
