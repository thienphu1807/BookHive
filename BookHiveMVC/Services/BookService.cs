using AutoMapper;
using BookHiveMVC;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Repository;
using BookHiveMVC.Repository.IRepository;

namespace BookHiveApi.Services
{
    public class BookService
    {
        private IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<ICollection<GetBook>> GetAllBook()
        {
            return await _bookRepository.GetAllBookAsync(ApiEndpoints.BookAPIPath);
        }
        public async Task<ICollection<GetBook>> GetBookByTitle(string title)
        {
            var book = await _bookRepository.GetBookByTitleAsync(ApiEndpoints.BookAPIPath, title);
            return book;
        }
        public async Task<ICollection<GetBook>> GetBookFromAuthor(int authorId)
        {
            var book = await _bookRepository.GetBookFromAuthorAsync(ApiEndpoints.BookAPIPath, authorId);
            return book;
        }
        public async Task<ICollection<GetBook>> GetBookFromCategory(int categoryId)
        {
            var book = await _bookRepository.GetBookFromCategoryAsync(ApiEndpoints.BookAPIPath, categoryId);
            return book;
        }
        public async Task<Book> GetBookById(int id)
        {
            return await _bookRepository.GetAsync(ApiEndpoints.BookAPIPath, id);
        }
        public async Task<bool> AddBook(CreateBook BookDtos)
        {
            var Book = _mapper.Map<Book>(BookDtos);
            return await _bookRepository.CreateAsync(ApiEndpoints.BookAPIPath, Book);
        }
        public async Task<bool> UpdateBook(int id, Book Book)
        {
            return await _bookRepository.UpdateAsync(ApiEndpoints.BookAPIPath + id, Book);
        }
        public async Task<bool> DeleteBook(int id)
        {
            return await _bookRepository.DeleteAsync(ApiEndpoints.BookAPIPath, id);
        }
    }
}
