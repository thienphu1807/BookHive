using AutoMapper;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Repository.IRepository;

namespace BookHiveMVC.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(IHttpClientFactory httpClientFactory, IMapper mapper) : base(httpClientFactory, mapper)
        {
        }
    }
}
