using AutoMapper;
using BookHiveMVC.Models;
using BookHiveMVC.Models.Dto;

namespace BookHiveMVC.Mapper
{
    public class BookHiveMapping : Profile
    {
        public BookHiveMapping()
        {
            CreateMap<Category, CreateCategory>().ReverseMap();
            CreateMap<Author, CreateAuthor>().ReverseMap();

            CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<CreateBook, Book>()
            .ForMember(ba => ba.BookAuthors, opt => opt.MapFrom(src =>
                src.AuthorIds.Select(id => new BookAuthor { AuthorId = id })))
            .ForMember(bc => bc.BookCategories, opt => opt.MapFrom(src =>
                src.CategoryIds.Select(id => new BookCategory { CategoryId = id })));

            CreateMap<Book, GetBook>()
            .ForMember(a => a.AuthorNames, opt => opt.MapFrom(src =>
            src.BookAuthors.Select(ba => ba.Author.Name).ToList()))
            .ForMember(c => c.CategoryNames, opt => opt.MapFrom(src =>
                src.BookCategories.Select(bc => bc.Category.Name).ToList()));

            CreateMap<UserBookReview, AddBookReview>()
                .ForMember(r => r.Rating, opt => opt.MapFrom(src => src.Rating));
        }
    }
}
