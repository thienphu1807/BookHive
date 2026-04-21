using BookHiveApi.Models;
using BookHiveApi.Models.Dtos;

namespace BookHiveApi.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        ICollection<Book> GetBooksByTitle(string title);
        bool AddRating(UserBookReview userBookReview);
        Task<List<GetBookReview>> GetBookRating(int bookId);
    }
}
