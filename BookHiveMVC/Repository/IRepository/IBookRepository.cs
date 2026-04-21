using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Models.Dtos;

namespace BookHiveMVC.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book> 
    {
        Task<bool> CreateBookAsync(string url, CreateBook createBook);
        Task<ICollection<GetBook>> GetAllBookAsync(string url);
        Task<ICollection<GetBook>> GetBookByTitleAsync(string url, string tilte);
        Task<ICollection<GetBook>> GetBookFromAuthorAsync(string url, int AuthorId);
        Task<ICollection<GetBook>> GetBookFromCategoryAsync(string url, int CategoryId);
        Task<ICollection<GetBookReview>> GetBookReview(string url, int bookId);
        Task<bool> AddBookReviewAsync(string url, int bookId, AddBookReview reviewDto);
    }
}
