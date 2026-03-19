using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReactAppBlog.Server.Application.DTOs.Posts;
using ReactAppBlog.Server.Application.Interfaces;
using ReactAppBlog.Server.Infrastructure.Persistence;
using AutoMapper.QueryableExtensions;
using ReactAppBlog.Server.Mapping;
using ReactAppBlog.Server.Domain.Entities;

namespace ReactAppBlog.Server.Application.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public PostService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResult<PostResponse>> GetPostsAsync(
            int page, 
            int pageSize, 
            string? keyword, 
            int? categoryId
        ) 
        {
            var query = _context.Posts
                .AsNoTracking()
                .Where(p => p.IsPublished);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(p => p.Title.Contains(keyword) || p.Content.Contains(keyword));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            var total = await query.CountAsync();

            var posts = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<PostResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<PostResponse>
            {
                Data = posts,
                Total = total,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<PostResponse?> GetPostByIdAsync (int id)
        {
            var post = await _context.Posts
                .AsNoTracking()
                .Where(p => p.Id == id && p.IsPublished)
                .ProjectTo<PostResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return post;
        }

        public async Task<PostResponse> CreatePostAsync(CreatePostRequest request, int userId)
        {
            var post = _mapper.Map<Post>(request);

            post.AuthorId = userId;
            post.CreatedAt = DateTime.UtcNow;

            if (request.TagIds.Any())
            {
                var tags = await _context.Tags
                    .Where(t => request.TagIds.Contains(t.Id))
                    .ToListAsync();

                post.PostTags = tags.Select(tag => new PostTag
                {
                    TagId = tag.Id,
                    Post = post
                }).ToList();
            }

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return _mapper.Map<PostResponse>(post);
        }
    }
}
