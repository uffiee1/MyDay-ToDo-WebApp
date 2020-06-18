using System.Threading.Tasks;
using MyDayApp.Models;

namespace MyDayApp.BusinessLogic.AdminLogic.Interfaces
{
    public interface IRoleLogic
    {
        void RoleCheck();

        Task<User> CreateRole(User user);
    }
}