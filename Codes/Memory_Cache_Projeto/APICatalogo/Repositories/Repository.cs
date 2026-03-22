using APICatalogo.Context;
using System.Linq.Expressions;

namespace APICatalogo.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        // Adiciona um delay de 3 segundos
        // Ele é sincrono e bloqueia a thread
        // Usar Task.Delay seria mais indicado
        System.Threading.Thread.Sleep(3000);
        return _context.Set<T>().ToList();
    }

    public T? Get(Expression<Func<T, bool>> predicate)
    {
        // Adiciona um delay de 3 segundos
        // Ele é sincrono e bloqueia a thread
        // Usar Task.Delay seria mais indicado
        System.Threading.Thread.Sleep(3000);
        return _context.Set<T>().FirstOrDefault(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return entity;
    }
    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        //_context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
        return entity;
    }
    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
        return entity;
    }
}
