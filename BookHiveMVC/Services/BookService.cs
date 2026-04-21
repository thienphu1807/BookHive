using AutoMapper;
using BookHiveMVC;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;
using BookHiveMVC.Models.Dtos;
using BookHiveMVC.Repository;
using BookHiveMVC.Repository.IRepository;

namespace BookHiveMVC.Services
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
            var book = await _bookRepository.GetBookFromAuthorAsync(ApiEndpoints.BookAPIPath+ "author/", authorId);
            return book;
        }
        public async Task<ICollection<GetBook>> GetBookFromCategory(int categoryId)
        {
            var book = await _bookRepository.GetBookFromCategoryAsync(ApiEndpoints.BookAPIPath + "category/", categoryId);
            return book;
        }
        public async Task<ICollection<GetBookReview>> GetBookReview(int bookId)
        {
            var book = await _bookRepository.GetBookReview(ApiEndpoints.BookAPIPath, bookId);
            return book;
        }
        public async Task<bool> AddBookRating(int bookId,AddBookReview addBookReview)
        {
            var success = await _bookRepository.AddBookReviewAsync(ApiEndpoints.BookAPIPath, bookId, addBookReview);

            if (success)
            {
                return true;
            }
            return false;
        }
        public async Task<Book> GetBookById(int id)
        {
            return await _bookRepository.GetAsync(ApiEndpoints.BookAPIPath, id);
        }
        public async Task<bool> AddBook(CreateBook BookDtos)
        {
            //var Book = _mapper.Map<Book>(BookDtos);
            return await _bookRepository.CreateBookAsync(ApiEndpoints.BookAPIPath, BookDtos);
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
