namespace ReactAppBlog.Server.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public bool IsPublished { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public User Author { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public List<PostTag> PostTags { get; set; } = new List<PostTag>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
