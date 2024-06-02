using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BikeRental.BusinessLogic.DataBase;
using BikeRental.Domain.Entities.User;
using BikeRental.Web.Models;
using UserEdit = BikeRental.Web.Models.Admin.UserEdit;

namespace BikeRental.Web.Services
{
    public class UserService
    {
        private readonly BikeContext _dbContext;

        public UserService()
        {
            _dbContext = new BikeContext();
        }

        public List<UserDBTable> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public UserDBTable GetById(int userId)
        {
            return _dbContext.Users.Single(u => u.Id == userId);
        }

        public UserDBTable UpdateUser(int userId, UserEdit userEdit)
        {
            var user = _dbContext.Users.Single(u => u.Id == userId);

            user.Name = userEdit.Name;
            user.Email = userEdit.Email;

            if (userEdit.NewPassword != null)
            {
                user.Password = userEdit.NewPassword;
            }

            _dbContext.SaveChanges();

            return user;
        }

        public void DeleteUser(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }
    }
}