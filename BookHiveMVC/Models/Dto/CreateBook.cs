namespace BookHiveMVC.Models.Dto
    public class CreateBook
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublishDate { get; set; }

        // Danh sách các ID để thiết lập mối quan hệ N:M
        public ICollection<int> AuthorIds { get; set; } = new List<int>();
        public ICollection<int> CategoryIds { get; set; } = new List<int>();
    }
}
