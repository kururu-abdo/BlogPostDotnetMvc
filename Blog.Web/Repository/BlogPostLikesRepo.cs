using System;
using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repository
{
	public class BlogPostLikesRepo : IBlogPostLikesRepo
	{
        private readonly BlogDbContext _blogDbContext;

        public BlogPostLikesRepo(BlogDbContext blogDbContext)
		{
         _blogDbContext = blogDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await _blogDbContext.BlogPostLikes.AddAsync(blogPostLike);
            await _blogDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<int> GetBlogPostTotalLikes(Guid blogpostId)
        {
            return await _blogDbContext.BlogPostLikes.CountAsync(
                x => x.BlogPostId== blogpostId
                );
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
          return   await _blogDbContext.BlogPostLikes.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }
    }
}

