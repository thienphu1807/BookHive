using System.ComponentModel.DataAnnotations;

namespace BookHiveMVC.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }  
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
       

        //M:N with Author
        public ICollection<BookAuthor> BookAuthors { get; set; }
        //M:N with Category
        public ICollection<BookCategory> BookCategories { get; set; }
        //M:N with Review
        public ICollection<UserBookReview> UserBookReviews { get; set; }
    }
}
