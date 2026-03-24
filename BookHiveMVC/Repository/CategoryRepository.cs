using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Repository.IRepository;

namespace BookHiveMVC.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }
    }
}
