using ReactAppBlog.Server.Migrations;

namespace ReactAppBlog.Server.Application.DTOs.Posts
{
    public class PostResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new();
        public int CommentCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
