using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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
                TotalPrice = CalculateFinalPrice(addReservation.BikeId, addReservation.DateStart, addReservation.DateEnd)
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

        public void DeleteById(int id)
        {
            var reservation = _dbContext.Reservations.FirstOrDefault(r => r.ReservationId == id && !r.Paid);
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

        internal decimal CalculateFinalPrice(int BikeId, DateTime startDateTime, DateTime endDateTime)
        {
            decimal price = 0;

            var bike = _dbContext.Bikes.FirstOrDefault(r => r.BikeId == BikeId);
            if (bike != null)
            {
                int daysNum = (int)(endDateTime - startDateTime).TotalDays;
                price = daysNum * bike.PricePerDay;
            }

            return price;
        }
        public ReservationDBTable UpdateReservation(int reservationId, ReservationEdit reservationEdit)
        {
            var reservation = _dbContext.Reservations.Single(r => r.ReservationId == reservationId);

            reservation.StartDate = reservationEdit.StartDate;
            reservation.EndDate = reservationEdit.EndDate;


            reservation.TotalPrice = CalculateFinalPrice(reservation.BikeId, reservation.StartDate, reservation.EndDate);

            _dbContext.SaveChanges();

            return reservation;
        }
    }

}