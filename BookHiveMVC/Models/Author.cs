using System.ComponentModel.DataAnnotations;

namespace BookHiveMVC.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }

        //M:N with Book

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
