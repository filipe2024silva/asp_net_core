using System.Linq.Expressions;

namespace APICatalogo.Repositories;

public interface IRepository<T>
{
    //cuidado para não violar o principio ISP
    IEnumerable<T> GetAll();
    T? Get(Expression<Func<T, bool>> predicate);// Expression<Func<T, bool>> usado para poder receber expressões lambda e retornar um boolean conforme o predicado, que é a condição usada para filtrar ex: _repo.Get(c => c.CategoriaId == id);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}
