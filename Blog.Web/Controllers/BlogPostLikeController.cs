using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikesRepo _blogPostLikesRepo;

        public BlogPostLikeController(IBlogPostLikesRepo blogPostRepository)
        {
            _blogPostLikesRepo = blogPostRepository;
        }



        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            var model = new BlogPostLike
            {
                BlogPostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserId
            };

            await _blogPostLikesRepo.AddLikeForBlog(model);
            return Ok();
        }

        [HttpGet]
        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {
            var total = await _blogPostLikesRepo.GetBlogPostTotalLikes(blogPostId);
            return Ok(total);

        }

    }
}