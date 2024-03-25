using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.ViewModels
{
	public class LoginViewModel
	{


        [Required]
        public string UserName { get; set; }

        [Required]
        [MinLength(6 , ErrorMessage ="password must be at least 6 characters")]
        public string Password { get; set; }


        public string? ReturnUrl { get; set; }
    }
}

