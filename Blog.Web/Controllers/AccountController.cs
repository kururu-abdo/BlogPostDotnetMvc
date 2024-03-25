using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser>
            _signInManager;

        public AccountController(UserManager<IdentityUser>
            userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
           _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            Console.WriteLine("Regsiter11");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel request)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new IdentityUser
                {
                    UserName = request.UserName,
                    Email = request.Email
                };

                //create a user
                var identityResult = await _userManager.CreateAsync(identityUser, request.Password);


                if (identityResult.Succeeded)
                {
                    //add role
                    var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "User");


                    if (roleIdentityResult.Succeeded)
                    {
                        //show success
                        return RedirectToAction("Register");

                    }
                    {
                        ModelState.AddModelError(nameof(RegisterViewModel.UserName), "User name already exists");

                        return View();
                    }
                } 

            }else
            {
                ModelState.AddModelError(nameof(RegisterViewModel.UserName), "User name already exists");

                return View();
            }


            //show error
            return View();


        }


        [HttpGet]
        public IActionResult Login(String ReturnUrl)

            
        {
            var model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            var signInResult=   await _signInManager.PasswordSignInAsync(
               request.UserName,
                request.Password,
               false , false
                );


            if (signInResult != null && signInResult.Succeeded)
            {
                if (!String.IsNullOrEmpty(request.ReturnUrl))
                {
                    return Redirect(request.ReturnUrl);
                }


                return RedirectToAction("Index", "Home");
            }


            //show errors
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
         await   _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }


        [HttpGet]
        public  IActionResult AccessDenied()
        {
            return View();
        }
    }
}