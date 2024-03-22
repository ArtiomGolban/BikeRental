using BikeRental.Domain.Entities.User;

namespace BikeRental.BusinessLogic.Core
{
    public class UserApi
    {
        internal ULoginData userRegisterlogic(ULoginData data)
        {
            return new ULoginData { Status = true };
        }
    }
}