using System;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data
{
	public class BlogDbContext :DbContext
	{
		public BlogDbContext(DbContextOptions<BlogDbContext> options): base(options)
		{
		}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql("Server=localhost;Database=blogdb;User=root;Password=;", ServerVersion.AutoDetect("Server=localhost;Database=blogdb;User=root;Password=;"));

        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostLikes { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set; }

    }
}

