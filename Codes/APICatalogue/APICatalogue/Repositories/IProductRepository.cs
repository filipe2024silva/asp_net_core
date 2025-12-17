using Models;
using Pagination;
using X.PagedList;

namespace Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        //IEnumerable<Product> GetProducts(ProductsParameters productsParameters);
        Task<IPagedList<Product>> GetProductsAsync(ProductsParameters productsParameters);
        Task<IPagedList<Product>> GetProductsFilterPriceAsync(ProductsFilterPrice productsFilterParams);
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);
    }
}
