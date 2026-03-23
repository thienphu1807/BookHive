using BookHiveApi.Models;

namespace BookHiveApi.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        ICollection<Book> GetBooksByTitle(string title);
        bool AddRating(UserBookReview userBookReview);
    }
}
