using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Repository.IRepository;

namespace BookHiveMVC.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }
    }
}
