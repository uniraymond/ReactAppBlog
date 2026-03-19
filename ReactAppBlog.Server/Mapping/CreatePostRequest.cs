using System.ComponentModel.DataAnnotations;

namespace ReactAppBlog.Server.Mapping
{
    public class CreatePostRequest
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public int CategoryId { get; set; }
        public List<int> TagIds { get; set; } = new();
        public bool IsPublished { get; set; }
    }
}
