using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDayApp.Models;

namespace MyDayApp.BusinessLogic.AccountLogic.Interfaces
{
    public interface IRegisterLogic
    { 
        Task<User> Register(User model);
    }
}