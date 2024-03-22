using BikeRental.BusinessLogic.Core;
using BikeRental.BusinessLogic.Interfaces;
using BikeRental.Domain.Entities.User;

namespace BikeRental.BusinessLogic
{
    public class SessionBL: UserApi, ISession
    {
        public ULoginData UserLogin(ULoginData userAuthLogic)
        {
            return userAuthLogic;
        }

        public ULoginData UserRegister(ULoginData userAuthLogic)
        {
            return userRegisterlogic(userAuthLogic);
        }
    }
}