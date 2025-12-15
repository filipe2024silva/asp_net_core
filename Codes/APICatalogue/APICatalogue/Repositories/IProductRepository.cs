using Models;
using System.Numerics;

namespace Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsByCategory(int categoryId);
    }
}
