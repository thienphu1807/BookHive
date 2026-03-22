using BookHiveApi.Data;
using BookHiveApi.Models;
using BookHiveApi.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookHiveApi.Repository
{
    public class AuthorRepository : Repository<Author>
    {
        public AuthorRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public ICollection<Book> GettBookFromAuthor(int  AuthorId)
        {
            return _appDbContext.Books.Include(ba => ba.BookAuthors).ThenInclude(a => a.Author )
                .Where(b => b.BookAuthors.Any(ba => ba.AuthorId == AuthorId)).ToList();
        }
    }
}
