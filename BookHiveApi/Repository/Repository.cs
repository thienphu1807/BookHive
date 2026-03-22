using BookHiveApi.Data;
using System.Linq.Expressions;

namespace BookHiveApi.Repository
{
    public class Repository<T> : IRepository.IRepository<T> where T : class
    {
        protected readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public virtual bool Add(T entity)
        {
            _appDbContext.Add<T>(entity);
            return SaveChange();
        }
        public virtual bool Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
            return SaveChange();
        }
        public virtual T Get(int id)
        {
            return _appDbContext.Find<T>(id);
        }
        public virtual ICollection<T> GetAll()
        {
            return _appDbContext.Set<T>().ToList();
        }
        public virtual ICollection<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _appDbContext.Set<T>().AsQueryable().Where(predicate).ToList();
        }
        public virtual bool Delete(int id)
        {
            var entityTemp = _appDbContext.Find<T>(id);
            if (entityTemp != null)
            {
                _appDbContext.Remove<T>(entityTemp);
            }
            return SaveChange();
        }
        public virtual bool SaveChange()
        {
            return _appDbContext.SaveChanges() >= 0 ? true : false;
        }
        public virtual bool HasValue(int id)
        {
            return _appDbContext.Set<T>().Find(id) != null;
        }
    }
}
