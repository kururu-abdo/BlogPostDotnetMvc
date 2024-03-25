using System;
using Azure;
using Microsoft.EntityFrameworkCore;
using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Repository
{
	public class TagRepository: ITagInterface
	{
        private readonly BlogDbContext _blogDbContext;

        public TagRepository(BlogDbContext blogDbContext)
		{
           _blogDbContext = blogDbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await _blogDbContext.Tags.AddAsync(tag);
            await _blogDbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var tag =await _blogDbContext.Tags.FindAsync(id);


            if (tag != null)
            {
                _blogDbContext.Tags.Remove(tag);
                await _blogDbContext.SaveChangesAsync();


                //show success
                return tag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await   _blogDbContext.Tags.ToListAsync();

        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            var existingTag =await _blogDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

            return existingTag;
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await _blogDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await _blogDbContext.SaveChangesAsync();
                return existingTag;

            }

            return null;

        }
    }
}

