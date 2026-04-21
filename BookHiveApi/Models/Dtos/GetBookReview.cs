namespace BookHiveApi.Models.Dtos
{
    public class GetBookReview
    {
        public string UserName { get; set; }
        public string BookTitle { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
    }
}
