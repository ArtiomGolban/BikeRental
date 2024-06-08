using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BikeRental.BusinessLogic.DataBase;
using BikeRental.Domain.Entities.Bikes;
using BikeRental.Domain.Entities.User;
using BikeRental.Domain.Enums;
using BikeRental.Web.Models;
using BikeRental.Web.Models.Admin;

namespace BikeRental.Web.Services
{
    public class BikeService
    {
        private readonly BikeContext _dbContext;

        public BikeService()
        {
            _dbContext = new BikeContext();
        }

        public List<BikeDBTable> GetAll()
        {
            return _dbContext.Bikes.ToList();
        }

        public BikeDBTable GetById(int bikeId)
        {
            return _dbContext.Bikes.Single(b => b.BikeId == bikeId);
        }

        public BikeDBTable AddBike(BikeAdd bikeAdd)
        {
            var bikeExist = _dbContext.Users.Any(u => u.Name == bikeAdd.Name);

            if (bikeExist)
            {
                return null;
            }

            var newBike = new BikeDBTable()
            {
                Name = bikeAdd.Name,
                Type = bikeAdd.Type, 
                PricePerDay = bikeAdd.PricePerDay,
                PhotoUrl = bikeAdd.PhotoUrl
            };

            _dbContext.Bikes.Add(newBike);
            _dbContext.SaveChanges();

            return newBike;
        }

        public BikeDBTable UpdateBike(int bikeId, BikeEdit bikeEdit)
        {
            var bike = _dbContext.Bikes.Single(u => u.BikeId == bikeId);

            bike.Name = bikeEdit.Name;
            bike.Type = bikeEdit.Type;
            bike.PricePerDay = bikeEdit.PricePerDay;
            bike.PhotoUrl = bikeEdit.PhotoUrl;

            _dbContext.SaveChanges();

            return bike;
        }

        public void DeleteBike(int bikeId)
        {
            var bike = _dbContext.Bikes.FirstOrDefault(u => u.BikeId == bikeId);
            if (bike != null)
            {
                _dbContext.Bikes.Remove(bike);
                _dbContext.SaveChanges();
            }
        }
    }

}