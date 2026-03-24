using System.ComponentModel.DataAnnotations;

namespace BookHiveMVC.Models
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }   

        //M:N with Book

        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
