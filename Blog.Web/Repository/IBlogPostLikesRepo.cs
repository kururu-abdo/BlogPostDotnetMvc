using System;
using Blog.Web.Models.Domain;

namespace Blog.Web.Repository
{
	public interface IBlogPostLikesRepo
	{

		Task<int> GetBlogPostTotalLikes(Guid blogpostId);

		Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);

		Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);

	}
}

