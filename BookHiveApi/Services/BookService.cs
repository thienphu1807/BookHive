using AutoMapper;
using BookHiveApi.Models;
using BookHiveApi.Models.Dtos;
using BookHiveApi.Repository;
using BookHiveApi.Repository.IRepository;

namespace BookHiveApi.Services
{
    public class BookService
    {
        private BookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(BookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public ICollection<Book> GetAllBook()
        {
            return _bookRepository.GetAll();
        }
        public ICollection<Book> GetBookByTitle(string title)
        {
             var book = _bookRepository.GetBooksByTitle(title);
            return book;
        }
        public Book GetBookById(int id)
        {
            return _bookRepository.Get(id);
        }
        public bool AddBook(CreateBook bookDtos)
        {
            var book = _mapper.Map<Book>(bookDtos);
            return _bookRepository.Add(book);
        }
        public bool AddBookRating(int bookId, string userId, AddBookReview AddBookReview)
        {
            var review = _mapper.Map<UserBookReview>(AddBookReview);
            review.BookId = bookId;
            review.UserId = userId;
            return _bookRepository.AddRating(review);
        }
        public bool UpdateBook(Book book)
        {
            return _bookRepository.Update(book);
        }
        public bool DeleteBook(int id)
        {
            return _bookRepository.Delete(id);
        }
        public bool HasBook(int id)
        {
            return _bookRepository.HasValue(id);
        }
    }
}
