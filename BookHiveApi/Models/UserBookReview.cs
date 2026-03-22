namespace BookHiveApi.Models
{
    public class UserBookReview
    {
        public string Description {  get; set; }
        public Rating Rating { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        public string UserId { get; set; }
        public User User{ get; set; }
    }
}
