using MyDayApp.BusinessLogic.AccountLogic.Interfaces;
using MyDayApp.BusinessLogic.AccountLogic;

namespace MyDayApp.BusinessLogic.UserLogic.Interfaces
{
    public interface IUserLogic
    {
        IRegisterLogic Register { get; }
        ILoginLogic Login { get; }
        ILogoutLogic Logout { get; }
    }
}