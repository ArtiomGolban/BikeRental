using System.ComponentModel.DataAnnotations;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BikeRental.BusinessLogic.DataBase;
using BikeRental.BusinessLogic.DBModel;
using BikeRental.Domain.Entities.Bikes;
using BikeRental.Domain.Entities.User;
using BikeRental.Domain.Enums;
using BikeRental.Helpers;

public class UserApi
{
    public ULoginData UserRegisterLogic(ULoginData data)
    {
        using (var context = new BikeContext())
        {
            if (context.Users.Any(u => u.Name == data.Username))
            {
                return new ULoginData { Status = false };
            }

            var newUser = new UserDBTable
            {
                Name = data.Username,
                Password = data.Password, // Remember to hash passwords in a real application
                Email = data.Email,
                Level = UserRole.User
            };

            context.Users.Add(newUser);
            context.SaveChanges();

            return new ULoginData { Status = true };
        }
    }

    public ULoginData UserLoginLogic(ULoginData data)
    {
        using (var context = new BikeContext())
        {
            var user = context.Users.FirstOrDefault(u => u.Name == data.Username && u.Password == data.Password);
            if (user == null)
            {
                return new ULoginData { Status = false };
            }
        }

        return new ULoginData { Status = true };
    }

    internal HttpCookie Cookie(string loginCredential)
    {
        var apiCookie = new HttpCookie("X-KEY")
        {
            Value = CookieGenerator.Create(loginCredential)
        };

        using (var db = new SessionContext())
        {
            SessionDBTable curent;

            curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();

            if (curent != null)
            {
                curent.CookieString = apiCookie.Value;
                curent.ExpireTime = DateTime.Now.AddMinutes(60);
                using (var sc = new SessionContext())
                {
                    sc.Entry(curent).State = EntityState.Modified;
                    sc.SaveChanges();
                }
            }
            else
            {
                db.Sessions.Add(new SessionDBTable
                {
                    Username = loginCredential,
                    CookieString = apiCookie.Value,
                    ExpireTime = DateTime.Now.AddMinutes(60)
                });
                db.SaveChanges();
            }
        }

        return apiCookie;
    }


    internal UserDBTable UserCookie(string cookie)
    {
        SessionDBTable session;
        UserDBTable curentUser;

        using (var db = new SessionContext())
        {
            session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
        }

        if (session == null) return null;
        using (var db = new BikeContext())
        {
            curentUser = db.Users.FirstOrDefault(u => u.Name == session.Username);
        }

        if (curentUser == null) return null;

        return curentUser;
    }

    internal ReservationResp BikeRentLogic(ReservationData data)
    {
        using (var db = new BikeContext())
        {
            var overlappingBookings = db.Reservations.Any(b => b.BikeId == data.BikeID.BikeId &&
                                                                b.StartDate < data.EndDateTime && b.EndDate > data.StartDateTime);

            var Bike = db.Bikes.FirstOrDefault(b => b.BikeId == data.BikeID.BikeId);
            if (!overlappingBookings)
            {
                var newReservation = new ReservationDBTable()
                {
                    StartDate = data.StartDateTime,
                    EndDate = data.EndDateTime,
                    TotalPrice = CalculateFinalPrice(data),
                    BikeId = Bike.BikeId
                };

                db.Reservations.Add(newReservation);
                db.SaveChanges();

                return new ReservationResp() { Status = true }; //success
            }
        }
        return new ReservationResp() { Status = false };

    }

    internal decimal CalculateFinalPrice(ReservationData data)
    {
        int price = 0;
        using (var context = new BikeContext())
        {
            var Bike = context.Bikes.FirstOrDefault(r => r.BikeId == data.BikeID.BikeId);
            if (Bike != null)
            {
                int numofDays = (int)(data.EndDateTime - data.StartDateTime).TotalDays;
                price = numofDays * Bike.PricePerDay;
            }
        }
        return price;
    }

}