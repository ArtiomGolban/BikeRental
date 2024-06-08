using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using BikeRental.BusinessLogic.DataBase;
using BikeRental.Domain.Entities.Bikes;
using BikeRental.Domain.Entities.User;
using BikeRental.Domain.Enums;
using BikeRental.Web.Models;
using BikeRental.Web.Models.Admin;

namespace BikeRental.Web.Services
{
    public class ReservationService
    {
        private readonly BikeContext _dbContext;

        public ReservationService()
        {
            _dbContext = new BikeContext();
        }

        public List<ReservationDBTable> GetAll()
        {
            return _dbContext.Reservations.ToList();
        }

        public ReservationDBTable GetById(int id)
        {
            return _dbContext.Reservations.Single(r => r.ReservationId == id);
        }

        public List<ReservationDBTable> GetAllByUser(UserDBTable user)
        {
            return _dbContext.Reservations.Where(r => r.UserId == user.Id).ToList();
        }

        public ReservationDBTable AddReservation(UserDBTable owner, ReservationCreate addReservation)
        {
            var exist = _dbContext.Reservations.Any(r => r.BikeId == addReservation.BikeId &&
                                                         r.StartDate < addReservation.DateEnd && r.EndDate > addReservation.DateStart);

            if (exist)
            {
                return null;
            }
        
            var newReservation = new ReservationDBTable()
            {
                BikeId = addReservation.BikeId,
                StartDate = addReservation.DateStart,
                EndDate = addReservation.DateEnd,
                UserId = owner.Id,
                TotalPrice = CalculateFinalPrice(addReservation)
            };

            _dbContext.Reservations.Add(newReservation);
            _dbContext.SaveChanges();

            return newReservation;
        }
        public ReservationDBTable GetForUserById(UserDBTable user, int id)
        {
            return _dbContext.Reservations.Single(r => r.ReservationId == id && r.UserId == user.Id);
        }

        public void DeleteForUserById(UserDBTable user, int id)
        {
            var reservation = _dbContext.Reservations.FirstOrDefault(r => r.ReservationId == id && r.UserId == user.Id && !r.Paid);
            if (reservation != null)
            {
                _dbContext.Reservations.Remove(reservation);
                _dbContext.SaveChanges();
            }
        }

        public void PayForUserById(UserDBTable user, int id)
        {
            var reservation = _dbContext.Reservations.FirstOrDefault(r => r.ReservationId == id && r.UserId == user.Id && !r.Paid);
            var usr = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id);

            if (reservation != null)
            {
                reservation.Paid = true;
                usr.Balance -= reservation.TotalPrice;

                _dbContext.SaveChanges();
            }
        }

        internal decimal CalculateFinalPrice(ReservationCreate data)
        {
            decimal price = 0;

            var bike = _dbContext.Bikes.FirstOrDefault(r => r.BikeId == data.BikeId);
            if (bike != null)
            {
                int daysNum = (int)(data.DateEnd - data.DateStart).TotalDays;
                price = daysNum * bike.PricePerDay;
            }

            return price;
        }


    }

}