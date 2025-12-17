using Context;
using Models;
using Pagination;
using X.PagedList;
using X.PagedList.Extensions;

namespace Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {}

        public async Task<IPagedList<Product>> GetProductsAsync(ProductsParameters productsParameters)
        {
            var products = await GetAllAsync();
            var orderedProducts = products.OrderBy(p => p.Id).AsQueryable();
            var result = orderedProducts.ToPagedList(productsParameters.PageNumber, productsParameters.PageSize);

            return result;
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            var products = await GetAllAsync();
            var productsByCategory = products.Where(p => p.CategoryId == categoryId);
            return productsByCategory;
        }

        public async Task<IPagedList<Product>> GetProductsFilterPriceAsync(ProductsFilterPrice productsFilterParams)
        {
            var products = await GetAllAsync();

            if(productsFilterParams.Price.HasValue && !string.IsNullOrEmpty(productsFilterParams.PriceCritery))
            {
                switch (productsFilterParams.PriceCritery.ToLower())
                {
                    case "major":
                        products = products.Where(p => p.Price > productsFilterParams.Price.Value).OrderBy(p => p.Price);
                        break;
                    case "minor":
                        products = products.Where(p => p.Price < productsFilterParams.Price.Value);
                        break;
                    case "equal":
                        products = products.Where(p => p.Price == productsFilterParams.Price.Value);
                        break;
                    default:
                        break;
                }
            }
            var filteredProducts = products.ToPagedList(productsFilterParams.PageNumber, productsFilterParams.PageSize);

            return filteredProducts;
        }
    }
}
