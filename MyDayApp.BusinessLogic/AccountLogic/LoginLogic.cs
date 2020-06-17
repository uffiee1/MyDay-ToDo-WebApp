using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyDayApp.BusinessLogic.AccountLogic.Interfaces;
using MyDayApp.Models;

namespace MyDayApp.BusinessLogic.AccountLogic
{
    public class LoginLogic : ILoginLogic
    {
        private readonly SignInManager<User> signInManager;

        public LoginLogic(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<User> Login(User model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.PasswordHash, false, false);
            if (result.Succeeded)
            {
                return model;
            }

            return model;
        }
    }
}