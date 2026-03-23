using AutoMapper;
using BookHiveApi.Models;
using BookHiveApi.Models.Dtos;
using BookHiveApi.Repository;
using BookHiveApi.Repository.IRepository;

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
        public ICollection<Author> GetAllAuthor()
        {
            return _authorRepository.GetAll();
        }
        public Author GetAuthorById(int id)
        {
            return _authorRepository.Get(id);
        }
        public bool AddAuthor(CreateAuthor authorDtos)
        {
            var author = _mapper.Map<Author>(authorDtos);
            return _authorRepository.Add(author);
        }
        public bool UpdateAuthor(Author author)
        {
            return _authorRepository.Update(author);
        }
        public bool DeleteAuthor(int id)
        {
            return _authorRepository.Delete(id);
        }
        public bool HasAuthor(int id)
        {
            return _authorRepository.HasValue(id);
        }
        public ICollection<Book> GetBooksFromAuthor(int AuthorId)
        {
            return _authorRepository.GettBookFromAuthor(AuthorId);
        }
    }
}
