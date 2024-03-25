using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{

  [Authorize(Roles ="Admin")]
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagInterface _tagRepo;
        private readonly IBlogPostRepository _blogRepo;

        public AdminBlogPostsController(ITagInterface
            tagRepo , IBlogPostRepository blogRepo)
        {
            _tagRepo = tagRepo;
            _blogRepo = blogRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await _tagRepo.GetAllAsync();


            var model = new AddBlogPostRequest
            {
Tags= tags.Select(x =>  new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
{

    Text=x.DisplayName, Value=x.Id.ToString()
})
            };


            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest request)
        {


            //map voiew model to domain model
            var blogPost = new BlogPost
            {
Heading=  request.Heading,
                PageTitle = request.PageTitle,
                Content = request.Content,
                ShortDescription = request.ShortDescription,

                FeaturedImageUrl = request.FeaturedImageUrl,

                UrlHandle = request.UrlHandle,
                PublishDate = request.PublishDate,
                Author = request.Author,
                Visible = request.Visible,


            };

            //map tag from selected tags
            var selectedTags = new List<Tag>();
          








            foreach (var item in request.SelectedTag)
            {
                var selectedTagGuid = Guid.Parse(item);

                var existingTag = await _tagRepo.GetAsync(selectedTagGuid);

                if (existingTag!= null )
                {
                    selectedTags.Add(existingTag);
                }

            }
            blogPost.Tags = selectedTags;

            await _blogRepo.AddAsync(blogPost);

            //show success alert

            return RedirectToAction("Add");
        }




        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogs = await _blogRepo.GetAllAsync();
            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blog = await _blogRepo.GetAsync(id);

            var tagsDomainModel = await _tagRepo.GetAllAsync();
       if(blog is not null)
            {

                var editBlogPost = new EditBlogPostRequest
                {
                    Id = blog.Id,
                    Heading = blog.Heading,
                    PageTitle = blog.PageTitle,
                    Content = blog.Content,
                    FeaturedImageUrl = blog.FeaturedImageUrl,

                    UrlHandle = blog.UrlHandle,
                    Author = blog.Author,

                    ShortDescription = blog.ShortDescription,
                    PublishDate = blog.PublishDate,
                    Visible = blog.Visible,
                    Tags = tagsDomainModel.Select(x =>

                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {

                        Value = x.Id.ToString(),
                        Text = x.Name
                    }
           ),
                    SelectedTag = blog.Tags.Select(x => x.Id.ToString()).ToArray()
                };

                return View(editBlogPost);
            }

            return View(null);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest request)
        {
            //map voiew model to domain model
            var blogPost = new BlogPost
            {

                Id=request.Id,
                Heading = request.Heading,
                PageTitle = request.PageTitle,
                Content = request.Content,
                ShortDescription = request.ShortDescription,

                FeaturedImageUrl = request.FeaturedImageUrl,

                UrlHandle = request.UrlHandle,
                PublishDate = request.PublishDate,
                Author = request.Author,
                Visible = request.Visible,


            };

            //map tag from selected tags
            var selectedTags = new List<Tag>();









            foreach (var item in request.SelectedTag)
            {
                var selectedTagGuid = Guid.Parse(item);

                var existingTag = await _tagRepo.GetAsync(selectedTagGuid);

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }

            }


            blogPost.Tags = selectedTags;

        var updatedBlog=    await _blogRepo.UpdateAsync(blogPost);
            if (updatedBlog is not null)
            {
                //show success alert

                return RedirectToAction("Edit");

            }
            //show failer alert

            return RedirectToAction("Edit");

        }




        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest request)
        {
            var deletedBlog = await _blogRepo.DeleteAsync(request.Id);

            if (deletedBlog is not null)
            {
                //show success request
                return RedirectToAction("List");
            }
            //show error

            return RedirectToAction("Edit" , new { id= request.Id});
        }

    }
}