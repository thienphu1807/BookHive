using BookHiveApi.Models;

namespace BookHiveApi.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        ICollection<Book> GettBookFromCategory(int categoryId);
    }
}
