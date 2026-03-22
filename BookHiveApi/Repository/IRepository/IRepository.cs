using System.Linq.Expressions;

namespace BookHiveApi.Repository.IRepository
{
    public interface IRepository<T>
    {
        bool Add(T entity);
        bool Update (T entity);
        T Get(int id);
        ICollection<T> GetAll();
        ICollection<T> Find(Expression<Func<T, bool>> predicate);
        bool Delete(int id);
        bool SaveChange();
    }
}
