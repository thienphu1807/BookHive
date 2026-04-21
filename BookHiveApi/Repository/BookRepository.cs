using BookHiveApi.Data;
using BookHiveApi.Models;
using BookHiveApi.Models.Dtos;
using BookHiveApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
            //userBookReview.Book = _appDbContext.Books.FromSql($@"SELECT * FROM Books WHERE Id = {userBookReview.BookId}").FirstOrDefault();
            //userBookReview.User = _appDbContext.Users.FromSql($@"SELECT * FROM AspNetUsers WHERE Id = {userBookReview.UserId}").FirstOrDefault();
            _appDbContext.UserBookReviews.Add(userBookReview);
            var bookReview = _appDbContext.Database.SqlQuery<ReviewBookCheck>($@"SELECT BookId, UserId FROM UserBookReviews WHERE BookId = {userBookReview.BookId} AND UserId = {userBookReview.UserId} GROUP BY BookId, UserId HAVING COUNT(*) > 0").ToList();
            if(bookReview.Count > 0)
            {
                return false;
            }
            return SaveChange();
        }
        public async Task<List<GetBookReview>> GetBookRating(int bookId)
        {
            FormattableString sql = $@"
                SELECT u.UserName, b.Title AS BookTitle, ubr.Description, ubr.Rating
                FROM AspNetUsers u
                INNER JOIN UserBookReviews ubr ON u.Id = ubr.UserId
                INNER JOIN Books b ON b.Id = ubr.BookId
                WHERE ubr.BookId = {bookId}";

            var reviews = await _appDbContext.Database
                .SqlQuery<GetBookReview>(sql)
                .ToListAsync();

            return reviews;
        }
    }
}
