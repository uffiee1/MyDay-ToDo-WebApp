using Microsoft.AspNetCore.Identity;
using MyDayApp.BusinessLogic.AccountLogic;
using MyDayApp.BusinessLogic.AccountLogic.Interfaces;
using MyDayApp.BusinessLogic.UserLogic.Interfaces;
using MyDayApp.Models;

namespace MyDayApp.BusinessLogic.UserLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IRoleLogic roleLogic;

        public UserLogic(SignInManager<User> signInManager, UserManager<User> userManager, IRoleLogic roleLogic)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleLogic = roleLogic;

            Register = new RegisterLogic(this.signInManager, this.userManager, this.roleLogic);
            Login = new LoginLogic(this.signInManager);
            Logout = new LogoutLogic(this.signInManager);
        }

        public IRegisterLogic Register { get; private set; }
        public ILoginLogic Login { get; private set; }
        public ILogoutLogic Logout { get; private set; }
    }
}