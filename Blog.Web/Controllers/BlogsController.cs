using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class BlogsController : Controller
    {

        private readonly IBlogPostRepository _blogRepo;
        private readonly IBlogPostLikesRepo _blogLikeRepo;

        private readonly IBlogPostCommentRepositoty _blogCommentRepo;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public BlogsController(IBlogPostRepository
            blogPostRepository ,
            IBlogPostLikesRepo blogPostLikes ,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser>  userManager ,

            IBlogPostCommentRepositoty blogCommentRepo
            )
        {
            _blogRepo = blogPostRepository;
            _blogLikeRepo = blogPostLikes;
            _signInManager = signInManager;
            _userManager = userManager;

            _blogCommentRepo = blogCommentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {

            var liked = false;
          var blog=  await _blogRepo.GetByUrlHandleAsync(urlHandle);
            var blogDetailsModel = new BlogDetailsViewModel();
            if (blog != null)
            {
                var totalLikes = await _blogLikeRepo.GetBlogPostTotalLikes(blog.Id);

                if (_signInManager.IsSignedIn(User))
                {
                    var likesForBlog = await _blogLikeRepo.GetLikesForBlog(blog.Id);

                    var userId = _userManager.GetUserId(User);
                    if (userId!=null)
                    {
                        var likesFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likesFromUser != null;

                    }

                }

                var blogComments = await _blogCommentRepo.GetCommentByBlogId(blog.Id);

                var commentsForViw = new List<BlogComment>();

                foreach (var blogComment in blogComments)
                {
                    commentsForViw.Add(new BlogComment
                    {
                        Description = blogComment.Description,
                        DateAdded = blogComment.DateAdded,
                        Username = (await _userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName

                    }) ;
                }

                 blogDetailsModel = new BlogDetailsViewModel
                {
                    Id =blog.Id ,

                    Content = blog.Content,
                    PageTitle = blog.PageTitle,
                    Author = blog.Author,
                    FeaturedImageUrl = blog.FeaturedImageUrl,


                    Heading = blog.Heading,

                    PublishDate = blog.PublishDate,
                    ShortDescription = blog.ShortDescription,
                    UrlHandle = blog.UrlHandle,
                    Visible = blog.Visible,

                    Tags = blog.Tags,
                    TotalLikes = totalLikes,
                    Liked =liked ,
                    Comments = commentsForViw
                 };
            }

            return View(blogDetailsModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
        {
            if (_signInManager.IsSignedIn(User))
            {

                var domainModel = new BlogPostComment
                {
                    BlogPostId = blogDetailsViewModel.Id,
                    Description = blogDetailsViewModel.CommentDescription,
                    UserId = Guid.Parse(_userManager.GetUserId(User)),
                    DateAdded = DateTime.Now

                };
                await _blogCommentRepo.AddAsync(domainModel);

                //return Ok();
                return RedirectToAction("Index", "Home", new { urlHandle = blogDetailsViewModel.UrlHandle });
            }

            return View();
        }


    }
}