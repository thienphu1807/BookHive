using BookHiveApi.Models;

namespace BookHiveApi.Repository.IRepository
{
    public interface IAuthorRepository : IRepository<Author>
    {
        ICollection<Book> GettBookFromAuthor(int AuthorId);
    }
}
