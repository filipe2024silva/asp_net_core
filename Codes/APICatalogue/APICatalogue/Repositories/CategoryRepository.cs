using Context;
using Models;
using Pagination;
using X.PagedList;
using X.PagedList.Extensions;

namespace Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IPagedList<Category>> GetCategoriesAsync(CategoriesParameters categoriesParameters)
        {
            var categories = await GetAllAsync();

            var orderedCategories = categories.OrderBy(c => c.Id).AsQueryable();

            var result = orderedCategories.ToPagedList(categoriesParameters.PageNumber, categoriesParameters.PageSize);//should be.ToPagedListAsync(categoriesParameters.PageNumber, categoriesParameters.PageSize);

            return result;
        }

        public async Task<IPagedList<Category>> GetCategoriesFilterNameAsync(CategoriesFilterName categoriesFilterName)
        {
            var categories = await GetAllAsync();

            if (!string.IsNullOrEmpty(categoriesFilterName.Name))
            {
                categories = categories.Where(c => c.Name.ToLower().Contains(categoriesFilterName.Name.ToLower()));
            }

            var filteredCategories = categories.ToPagedList(categoriesFilterName.PageNumber, categoriesFilterName.PageSize);//should be .ToPagedListAsync(categoriesFilterName.PageNumber, categoriesFilterName.PageSize);

            return filteredCategories;
        }
    }
}
