using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminUsersController(
            IUserRepository userRepository,

            UserManager<IdentityUser> userManager
            )
        {
            this.userRepository = userRepository;
            _userManager = userManager;
        }

        public UserManager<IdentityUser> UserManager { get; }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAll();
            var userViewModel = new UserViewModel();
            userViewModel.Users = new List<User>();
            foreach (var user in users)
            {
                userViewModel.Users.Add(
                    new Models.ViewModels.User
                    {


                        Id = Guid.Parse(user.Id),
                        Username = user.UserName,
                        EmailAddress = user.Email
                    }


                    );
            }

            return View(userViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> List(UserViewModel request)
        {

            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email

            };

            var identityResult = await _userManager.CreateAsync(identityUser, request.Password);
            if(identityResult is not null)
            {

                if (identityResult.Succeeded)
                {

                    var roles = new List<string>
                {
                    "User"
                };


                    if (request.AdminRoleCheckbox)
                    {
                        roles.Add("Admin");
                    }
                    identityResult = await _userManager.AddToRolesAsync(
                               identityUser,
                               roles);


                    if (identityResult is not null && identityResult.Succeeded)
                    {



                        return RedirectToAction("List", "AdminUsers");
                    }

                }

            }


            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is not null)
            {
                var identityResult = await _userManager.DeleteAsync(user);

                if (identityResult is not null && identityResult.Succeeded)
                {
                    return RedirectToAction("List", "AdminUsers");

                }
            }

            return View();
        }
    }
}