using BookHiveApi.Data;
using BookHiveApi.Models;
using BookHiveApi.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookHiveApi.Repository
{
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public ICollection<Book> GettBookFromCategory(int  categoryId)
        {
            return _appDbContext.Books.Include(bc => bc.BookCategories).ThenInclude(c => c.Category)
                .Include(ba => ba.BookAuthors).ThenInclude(a => a.Author)
                .Where(b => b.BookCategories.Any(bc => bc.CategoryId == categoryId)).ToList();
        }
    }
}
