using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyDayApp.BusinessLogic.AdminLogic.Interfaces;
using MyDayApp.Models;

namespace MyDayApp.BusinessLogic.AdminLogic
{
    public class RoleLogic : IRoleLogic
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleLogic(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<User> CreateRole(User user)
        {
            if (user.Role == null)
            {
                await userManager.AddToRoleAsync(user, Role.Gebruiker);
            }
            else
            {
                await userManager.AddToRoleAsync(user, user.Role);
            }

            return user;
        }

        public void RoleCheck()
        {
            if (roleManager.RoleExistsAsync(Role.Gebruiker) != null)
            { 
                roleManager.CreateAsync(new IdentityRole(Role.Gebruiker));
            }

            //Voor Admin Role
            //if (roleManager.RoleExistsAsync(Role.Administrator) != null)
            //{
            //    roleManager.CreateAsync(new IdentityRole(Role.Administrator));
            //}
        }
    }
}
