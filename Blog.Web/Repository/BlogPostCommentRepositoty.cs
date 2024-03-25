using System;
using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repository
{
	public class BlogPostCommentRepositoty: IBlogPostCommentRepositoty
	{
        private readonly BlogDbContext _blogDb;
		public BlogPostCommentRepositoty(BlogDbContext blogDb)

		{
            _blogDb = blogDb;
		}

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await _blogDb.BlogPostComments.AddAsync(blogPostComment);
            await _blogDb.SaveChangesAsync();

            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetCommentByBlogId(Guid blogPostId)
        {
         var comments =  await _blogDb.BlogPostComments.Where(x => x.BlogPostId == blogPostId).ToListAsync();
            return comments;
        }
    }
}

