﻿using System;
namespace Blog.Web.Models.ViewModels
{
	public class UserViewModel
	{
		public List<User> Users { get; set; } = new List<User>();


        public string Username { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public bool AdminRoleCheckbox { get; set; }

    }
}

