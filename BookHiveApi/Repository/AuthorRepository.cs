using BookHiveApi.Data;
using BookHiveApi.Models;
using BookHiveApi.Models.Dtos;
using BookHiveApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookHiveApi.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public ICollection<Book> GettBookFromAuthor(int AuthorId)
        {
            return _appDbContext.Books.
                Include(ba => ba.BookAuthors).ThenInclude(a => a.Author)
                                .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
                .Where(b => b.BookAuthors.Any(ba => ba.AuthorId == AuthorId)).ToList();
        }
    }
}
