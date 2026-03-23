using BookHiveApi.Data;
using BookHiveApi.Models;
using BookHiveApi.Models.Dtos;
using BookHiveApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BookHiveApi.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public override ICollection<Book> GetAll()
        {
            return _appDbContext.Books
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
                .ToList();
        }
        public ICollection<Book> GetBooksByTitle(string title)
        {
            return _appDbContext.Books.Where(b => b.Title.Contains(title))
                .Include(ba => ba.BookAuthors).ThenInclude(a => a.Author )
                .Include(bc => bc.BookCategories).ThenInclude(c => c.Category)
                .ToList();
        }
        public bool AddRating(UserBookReview userBookReview)
        {
            _appDbContext.UserBookReviews.Add(userBookReview);
            return SaveChange();
        }
    }
}
