using System.ComponentModel.DataAnnotations;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BikeRental.BusinessLogic.DataBase;
using BikeRental.BusinessLogic.DBModel;
using BikeRental.Domain.Entities.User;
using BikeRental.Domain.Enums;
using BikeRental.Helpers;


namespace BikeRental.BusinessLogic.Core
{
    public class AddUserResult
    {
        public bool Status { get; set; }
        public int UserId { get; set; }
    }

    public class AdminApi
    {
        public AddUserResult AddUserLogic(ULoginData userAdd)
        {
            using (var context = new BikeContext())
            {
                if (context.Users.Any(u => u.Name == userAdd.Username))
                {
                    return new AddUserResult { Status = false };
                }

                var newUser = new UserDBTable
                {
                    Name = userAdd.Username,
                    Password = userAdd.Password, // Remember to hash passwords in a real application
                    Email = userAdd.Email,
                    Level = UserRole.User
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                return new AddUserResult { Status = true, UserId = newUser.Id };
            }
        }
    }

}