using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data
{
	public class AuthDbContext: IdentityDbContext
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
		{
		}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql("Server=localhost;Database=blogAuthDb;User=root;Password=;", ServerVersion.AutoDetect("Server=localhost;Database=blogAuthDb;User=root;Password=;"));

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //seeds Roles(User ,Admin ,SuperAdmin)
            var adminRoleId = "283e968d-d000-4a18-88e7-66259d03e49d";
            var superAdminRoleId = "28c64bf2-8ee0-4bf9-ad0f-b0519670cb88";

            var userRoleId = "2616ef9e-773a-48eb-b01b-c3bf1f51f744";


            var roles = new List<IdentityRole>
            {

                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName="Admin",
                    Id=adminRoleId ,
                    ConcurrencyStamp= adminRoleId
                },

                 new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=superAdminRoleId ,
                    ConcurrencyStamp= superAdminRoleId
                },
                  new IdentityRole
                {
                    Name = "User",
                    NormalizedName="User",
                    Id=userRoleId ,
                    ConcurrencyStamp= userRoleId
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);


            //seed SuperAdmin
            var superAdminUserId = "99394798-e721-488e-8865-88f972541410";

            var superAdminUser = new IdentityUser
            {
                UserName = "super@admin.com",
                Email = "super@admin.com",
                NormalizedEmail = "super@admin".ToUpper(),
                NormalizedUserName = "super@admin".ToUpper(),
                Id = superAdminUserId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add all roles to SuperAdmin

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
new IdentityUserRole<string>
{
    RoleId=superAdminRoleId,

    UserId= superAdminUserId
},


new IdentityUserRole<string>
{
    RoleId=userRoleId,

    UserId= superAdminUserId
},
new IdentityUserRole<string>
{
    RoleId=adminRoleId,

    UserId= superAdminUserId
},

            };


            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);


        }
    }
}

