using System.Collections.Generic;
using System.Linq;
using CRUDforCar.Interfaces;
using CRUDforCar.Models;

namespace CRUDforCar.Services
{
    public class CarsPostgresService : IRepositoryCar
    {
        public CarsPostgresContext _db;

        /// <summary>
        /// Сервис работы с базой данных Postgres
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public CarsPostgresService(CarsPostgresContext context)
        {
            _db = context;
        }

        public IEnumerable<Car> GetItemList()
        {
            return _db.Cars;
        }

        public Car GetItem(string id)
        {
            return _db.Cars.FirstOrDefault(car => car.CarId == id);
        }

        public void Create(Car item)
        {
            _db.Cars.Add(item);
            _db.SaveChanges();
        }

        public void Update(Car item)
        {
            var car = _db.Cars.FirstOrDefault(car => car.CarId == item.CarId);
            if (car != null)
            {
                car.CarName = item.CarName ?? car.CarName;
                car.Description = item.Description ?? car.Description;

                _db.Cars.Update(car);
                _db.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            var car = _db.Cars.FirstOrDefault(car => car.CarId == id);
            if (car != null)
                _db.Cars.Remove(car);
            _db.SaveChanges();
        }
    }
}