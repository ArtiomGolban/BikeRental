using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BikeRental.BusinessLogic.DataBase;
using BikeRental.Domain.Entities.User;
using BikeRental.Domain.Enums;
using BikeRental.Web.Models;
using BikeRental.Web.Models.Admin;
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
            user.Balance = userEdit.Balance;

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

        public UserDBTable AddUser(UserAdd userAdd)
        {
            var userExist = _dbContext.Users.Any(u => u.Name == userAdd.Name);

            if (userExist)
            {
                return null;
            }
            var newUser = new UserDBTable
            {
                Name = userAdd.Name,
                Password = userAdd.Password, // Remember to hash passwords in a real application
                Email = userAdd.Email,
                Level = UserRole.User,
                Balance = userAdd.Balance
            };

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return newUser;
        }

        public void TopUpBalance(UserDBTable user, decimal amount)
        {
            var balance = _dbContext.Users.SingleOrDefault(u => u.Id == user.Id);
            if (balance != null)
            {
                balance.Balance += amount;
                _dbContext.SaveChanges();
            }
        }

        public UserDBTable EditEmail(int userId, string email)
        {
            var user = _dbContext.Users.Single(u => u.Id == userId);

            user.Email = email;

            _dbContext.SaveChanges();

            return user;
        }

        public UserDBTable ChangePassword(int userId, string password)
        {
            var user = _dbContext.Users.Single(u => u.Id == userId);

            user.Password = password;

            _dbContext.SaveChanges();

            return user;
        }

    }
}