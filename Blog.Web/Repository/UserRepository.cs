using System;
using Blog.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repository
{
	public class UserRepository :IUserRepository
	{
        private readonly AuthDbContext _authDbContext;

        public UserRepository(AuthDbContext authDbContext)
		{
            _authDbContext = authDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users= await _authDbContext.Users.ToListAsync();

            var superAdmin = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Email == "super@admin.com");

            if (superAdmin is not null)
            {
                users.Remove(superAdmin);
            }
            return users;
                
        }
    }
}

