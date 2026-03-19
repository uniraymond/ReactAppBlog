using ReactAppBlog.Server.Application.DTOs.Posts;
using ReactAppBlog.Server.Mapping;

namespace ReactAppBlog.Server.Application.Interfaces
{
    public interface IPostService
    {
        Task<PagedResult<PostResponse>> GetPostsAsync(
            int page, int pageSize, string? keyword, int? categoryId
        );

        Task<PostResponse> CreatePostAsync(CreatePostRequest request, int userId);
    }
}
