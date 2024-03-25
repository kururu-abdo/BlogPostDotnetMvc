using System;
using Blog.Web.Models.Domain;

namespace Blog.Web.Repository
{
	public interface IBlogPostCommentRepositoty
	{
		Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);

		Task<IEnumerable<BlogPostComment>> GetCommentByBlogId(Guid blogPostId);

	}
}

