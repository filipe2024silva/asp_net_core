using Context;
using Models;

namespace Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {}

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return GetAll().Where(p => p.CategoryId == categoryId);
        }
    }
}
