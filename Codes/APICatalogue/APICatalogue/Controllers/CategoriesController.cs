using APICatalogue.Filters;
using Repositories;
using Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using APICatalogue.DTOs.Mappings;
using DTOs;

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
        public ActionResult<IEnumerable<CategoryDTO>> Get()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();

            if (categories is null)
            {
                return NotFound("Categories not found");
            }
            var categoryDTO = categories.ToCategoryDTOList();

            return Ok(categoryDTO);
        }

        [HttpGet("{id:int:min(1)}", Name = "GetCategory")]
        public ActionResult<CategoryDTO> Get(int id)
        {
            var category = _unitOfWork.CategoryRepository.Get(c => c.Id == id);

            if (category is null)
            {
                return NotFound("Category not found");
            }
            var categoryDTO = category.ToCategoryDTO();

            return Ok(categoryDTO);
        }

        [HttpPost]
        public ActionResult<CategoryDTO> Post(CategoryDTO categoryDTO)
        {
            if (categoryDTO is null)
                return BadRequest();

            var category = categoryDTO.ToCategory();

            var categoryCreated = _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.Commit();

            var categoryCreatedDTO = categoryCreated.ToCategoryDTO();

            return new CreatedAtRouteResult("GetCategory", new { id = categoryCreatedDTO.Id }, categoryCreatedDTO);
        }

        [HttpPut("{id:int:min(1)}")]
        public ActionResult<CategoryDTO> Put(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
                return BadRequest();

            var category = categoryDTO.ToCategory();

            var updatedCategory = _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();

            var categoryUpdatedDTO = updatedCategory.ToCategoryDTO();

            return Ok(categoryUpdatedDTO);
        }

        [HttpDelete("{id:int:min(1)}")]
        public ActionResult<CategoryDTO> Delete(int id)
        {
            var category = _unitOfWork.CategoryRepository.Get(c => c.Id == id);

            if (category is null)
            {
                return NotFound("Category not found");
            }

            var deletedCategory = _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Commit();

            var deletedCategoryDTO = deletedCategory.ToCategoryDTO();

            return Ok(deletedCategoryDTO);
        }
    }
}
