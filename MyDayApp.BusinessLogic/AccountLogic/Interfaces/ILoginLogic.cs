using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyDayApp.Models;

namespace MyDayApp.BusinessLogic.AccountLogic.Interfaces
{
    public interface ILoginLogic
    {
        Task<User> Login(User model);
    }
}