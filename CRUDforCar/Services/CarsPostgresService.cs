using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDforCar.Interfaces;
using CRUDforCar.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDforCar.Services
{
    public class CarsPostgresService : ICarsDBService
    {
        private CarPostgresContext _db;

        public CarsPostgresService(ICarsDatabaseSettings settings)
        {
            _db = new CarPostgresContext(settings);
        }

        #region Create

        public Car Create(Car car)
        {
            _db.Cars.Add(car);
            _db.SaveChanges();
            return car;
        }

        #endregion

        #region Read

        public List<Car> Get() =>
            _db.Cars.ToList();

        public Car Get(string id) =>
            _db.Cars.FirstOrDefault( car => car.CarId == id);

        #endregion

        #region Update

        public void Update(string id, Car carIn)
        {
            var car = _db.Cars.FirstOrDefault(car => car.CarId == id);
            if (car != null)
            {
                car.CarName = carIn.CarName ?? car.CarName;
                car.Description = carIn.Description ?? car.Description;

                _db.Cars.Update(car);
            }
        }

        #endregion

        #region Delete

        public void Remove(Car carIn)
        {
            _db.Cars.RemoveRange(_db.Cars);
            _db.SaveChanges();
        }
            

        public void Remove(string id)
        {
            var car = _db.Cars.FirstOrDefault(car => car.CarId == id);
            _db.Cars.Remove(car);
            _db.SaveChanges();
        }

        #endregion
    }
}
