using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyDayApp.Models;

namespace MyDayApp.BusinessLogic.AccountLogic.Interfaces
{
    public interface IRoleLogic
    {
        void RoleCheck();

        Task<User> CreateRole(User user);
    }
}