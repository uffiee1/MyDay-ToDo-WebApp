using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using MyDayApp.BusinessLogic.AccountLogic.Interfaces;
using MyDayApp.Models;

namespace MyDayApp.BusinessLogic.AccountLogic
{
    public class LogoutLogic : ILogoutLogic
    {
        private readonly SignInManager<User> signInManager;

        public LogoutLogic(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async void Logout()
        {
            await signInManager.SignOutAsync();
        }
    }
}