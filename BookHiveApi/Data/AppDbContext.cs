using BookHiveApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookHiveApi.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authorities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookAuthor> BookAuthorities { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBookReview> UserBookReviews{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BookAuthor>().HasKey(ba => new {ba.BookId, ba.AuthorId});
            builder.Entity<BookCategory>().HasKey(bc => new { bc.BookId, bc.CategoryId });

            //Config Relationship Book - Author

            builder.Entity<BookAuthor>().HasOne(b => b.Book).WithMany(ba => ba.BookAuthors).HasForeignKey(ba => ba.BookId);
            builder.Entity<BookAuthor>().HasOne(a => a.Author).WithMany(ba => ba.BookAuthors).HasForeignKey(ba => ba.AuthorId);

            //config Relationship Book - Category
            builder.Entity<BookCategory>().HasOne(b => b.Book).WithMany(bc => bc.BookCategories).HasForeignKey(bc => bc.BookId);
            builder.Entity<BookCategory>().HasOne(c => c.Category).WithMany(bc => bc.BookCategories).HasForeignKey(bc => bc.CategoryId);

            //Config Relationship Book - UserReview
            builder.Entity<UserBookReview>().HasOne(b => b.Book).WithMany(br => br.UserBookReviews).HasForeignKey(br => br.BookId);
            builder.Entity<UserBookReview>().HasOne(u => u.User).WithMany(ur => ur.UserBookReviews).HasForeignKey(ur => ur.UserId);

            //config Role
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "85cf603a-6761-4603-99fd-f661f0000001", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "85cf603a-6761-4603-99fd-f661f0000002", Name = "Reader", NormalizedName = "READER" },
                new IdentityRole { Id = "85cf603a-6761-4603-99fd-f661f0000003", Name = "Reviewer", NormalizedName = "REVIEWER" }
                );
        }
    }
}
