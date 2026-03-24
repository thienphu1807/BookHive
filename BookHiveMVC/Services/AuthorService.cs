using AutoMapper;
using BookHiveMVC;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Repository;
using BookHiveMVC.Repository.IRepository;

namespace BookHiveApi.Services
{
    public class AuthorService
    {
        private IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }
        public async Task<ICollection<Author>> GetAllAuthor()
        {
            return await _authorRepository.GetAllAsync(ApiEndpoints.AuthorAPIPath);
        }
        public async Task<Author> GetAuthorById(int id)
        {
            return await _authorRepository.GetAsync(ApiEndpoints.AuthorAPIPath, id);
        }
        public async Task<bool> AddAuthor(CreateAuthor authorDtos)
        {
            var author = _mapper.Map<Author>(authorDtos);
            return await _authorRepository.CreateAsync(ApiEndpoints.AuthorAPIPath, author);
        }
        public async Task<bool> UpdateAuthor(Author author)
        {
            return await _authorRepository.UpdateAsync(ApiEndpoints.AuthorAPIPath, author);
        }
        public async Task<bool> DeleteAuthor(int id)
        {
            return await _authorRepository.DeleteAsync(ApiEndpoints.AuthorAPIPath, id);
        }
    }
}
