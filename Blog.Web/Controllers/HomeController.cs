using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models;
using Blog.Web.Repository;
using Blog.Web.Models.ViewModels;

namespace Blog.Web.Controllers;

public class HomeController : Controller
{
    private readonly IBlogPostRepository _blogRepo;
    private readonly ITagInterface _tagRepo;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger,

        IBlogPostRepository blogPostRepository,
        ITagInterface tagRepo
        )
    {
        _logger = logger;
        _blogRepo = blogPostRepository;
        _tagRepo = tagRepo;
    }

    public async Task<IActionResult> Index()
    {


        //get all blogs

        var blogs = await _blogRepo.GetAllAsync();


        //get all tags

        var tags = await _tagRepo.GetAllAsync();

        var homeModel = new HomeViewModel
        {
            BlogPosts = blogs,
            Tags = tags
        };
        return View(homeModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

