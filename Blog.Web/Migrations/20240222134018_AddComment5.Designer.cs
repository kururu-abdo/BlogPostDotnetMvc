﻿// <auto-generated />
using System;
using Blog.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Blog.Web.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20240222134018_AddComment5")]
    partial class AddComment5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Blog.Web.Models.Domain.BlogPost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FeaturedImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Heading")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PageTitle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Visible")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("Blog.Web.Models.Domain.BlogPostComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("BlogPostId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("BlogPostId");

                    b.ToTable("BlogPostComments");
                });

            modelBuilder.Entity("Blog.Web.Models.Domain.BlogPostLike", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("BlogPostId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("BlogPostId");

                    b.ToTable("BlogPostLikes");
                });

            modelBuilder.Entity("Blog.Web.Models.Domain.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("BlogPostTag", b =>
                {
                    b.Property<Guid>("BlogPostsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("char(36)");

                    b.HasKey("BlogPostsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("BlogPostTag");
                });

            modelBuilder.Entity("Blog.Web.Models.Domain.BlogPostComment", b =>
                {
                    b.HasOne("Blog.Web.Models.Domain.BlogPost", null)
                        .WithMany("Comments")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Blog.Web.Models.Domain.BlogPostLike", b =>
                {
                    b.HasOne("Blog.Web.Models.Domain.BlogPost", null)
                        .WithMany("Likes")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlogPostTag", b =>
                {
                    b.HasOne("Blog.Web.Models.Domain.BlogPost", null)
                        .WithMany()
                        .HasForeignKey("BlogPostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blog.Web.Models.Domain.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Blog.Web.Models.Domain.BlogPost", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}