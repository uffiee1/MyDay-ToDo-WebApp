using Microsoft.AspNetCore.Identity;
using MyDayApp.Models;
using MyDayApp.BusinessLogic.AccountLogic.Interfaces;
using System.Threading.Tasks;
using MyDayApp.BusinessLogic.AdminLogic.Interfaces;

namespace MyDayApp.BusinessLogic.AccountLogic
{
    public class RegisterLogic : IRegisterLogic
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IRoleLogic roleLogic;

        public RegisterLogic(SignInManager<User> signInManager, UserManager<User> userManager, IRoleLogic roleLogic)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleLogic = roleLogic;
        }

        public async Task<User> Register(User model)
        {
            roleLogic.RoleCheck();

            if (await userManager.FindByEmailAsync(model.UserName) == null)
            {
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email,
                    Role = model.Role,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync((User)user, model.PasswordHash);

                if (result.Succeeded)
                {
                    if (model.Role == null)
                    {
                        await userManager.AddToRoleAsync(user, Role.Gebruiker);
                    }

                    await roleLogic.CreateRole(user);

                    await userManager.AddToRoleAsync(user, model.Role);

                    await signInManager.SignInAsync(user, false);

                    return model;
                }

                return model;
            }

            return model;
        }
    }
}
