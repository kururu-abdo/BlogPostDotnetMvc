using System;
using Microsoft.AspNetCore.Identity;

namespace Blog.Web.Repository
{
	public interface IUserRepository
	{

		Task<IEnumerable<IdentityUser>> GetAll();
	}
}

