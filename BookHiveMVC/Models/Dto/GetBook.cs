namespace BookHiveMVC.Models.Dto
{
    public class GetBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }

        // Thay vì trả về cả Object BookAuthor, ta chỉ trả về tên hoặc ID đơn giản
        public ICollection<string> AuthorNames { get; set; } = new List<string>();
        public ICollection<string> CategoryNames { get; set; } = new List<string>();
    }
}
