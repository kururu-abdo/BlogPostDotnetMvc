using System;
using Blog.Web.Models.Domain;

namespace Blog.Web.Repository
{
	public interface IBlogPostRepository
	{

        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetAsync(Guid id);

        Task<BlogPost?> GetByUrlHandleAsync(string UrlHandle);
        Task<BlogPost?> AddAsync(BlogPost blog);
        Task<BlogPost?> UpdateAsync(BlogPost blog);
        Task<BlogPost?> DeleteAsync(Guid id);


    }
}

