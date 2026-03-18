namespace ReactAppBlog.Server.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<UserRole> UserRoles { get; set; } = new ();
        public List<RefreshToken> RefreshTokens { get; set; } = new ();
    }
}
