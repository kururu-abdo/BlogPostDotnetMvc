using System;
using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repository
{
	public class BlogPostRepository :IBlogPostRepository
	{
        private readonly BlogDbContext _blogDbContext;

        public BlogPostRepository(BlogDbContext blogDbContext)
		{
           _blogDbContext = blogDbContext;
        }

        public async Task<BlogPost?> AddAsync(BlogPost blog)
        {
            await _blogDbContext.AddAsync(blog);
            await _blogDbContext.SaveChangesAsync();

            return blog;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await _blogDbContext.BlogPosts.FindAsync(id);

            if (existingBlog is not null)
            {
                _blogDbContext.BlogPosts.Remove(existingBlog);
                await _blogDbContext.SaveChangesAsync();

                return existingBlog;
            }


            return null;

        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
        return  await  _blogDbContext.BlogPosts.Include(x=> x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return
                await _blogDbContext.BlogPosts.Include(x=> x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string UrlHandle)
        {
            return
                      await _blogDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.UrlHandle == UrlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blog)
        {
            var existiingBlog = await _blogDbContext.BlogPosts.Include(x => x.Tags)
                          .FirstOrDefaultAsync(x => x.Id == blog.Id);

            if (existiingBlog is not null)

            {
                existiingBlog.Id = blog.Id;

                existiingBlog.Heading = blog.Heading;
                existiingBlog.PageTitle = blog.PageTitle;
                existiingBlog.ShortDescription = blog.ShortDescription;
                existiingBlog.Content = blog.Content;
                existiingBlog.Author = blog.Author;
                existiingBlog.PublishDate = blog.PublishDate;
                existiingBlog.FeaturedImageUrl = blog.FeaturedImageUrl;
                existiingBlog.UrlHandle = blog.UrlHandle;
                existiingBlog.Visible = blog.Visible;
                existiingBlog.Tags = blog.Tags;

                await _blogDbContext.SaveChangesAsync();

                return existiingBlog;
            }
            return null;


        }
    }
}

