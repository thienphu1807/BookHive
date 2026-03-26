using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;

namespace BookHiveMVC.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book> 
    {
        Task<ICollection<GetBook>> GetAllBookAsync(string url);
        Task<ICollection<GetBook>> GetBookByTitleAsync(string url, string tilte);
        Task<ICollection<GetBook>> GetBookFromAuthorAsync(string url, int AuthorId);
        Task<ICollection<GetBook>> GetBookFromCategoryAsync(string url, int CategoryId);
    }
}
