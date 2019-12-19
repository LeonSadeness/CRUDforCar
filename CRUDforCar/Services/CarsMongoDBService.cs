using CRUDforCar.Interfaces;
using CRUDforCar.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace CRUDforCar.Services
{
    public class CarsMongoDBService : IRepositoryCar
    {
        private readonly IMongoCollection<Car> _db;

        /// <summary>
        /// Сервис работы с БД MongoDB
        /// </summary>
        /// <param name="con">Строка подключения</param>
        /// <param name="db">Имя базы данных</param>
        /// <param name="col">Имя коллекции</param>
        public CarsMongoDBService(string con, string db, string col)
        {
            var client = new MongoClient(con);
            var database = client.GetDatabase(db);
            _db = database.GetCollection<Car>(col);
        }

        public IEnumerable<Car> GetItemList()
        {
            return _db.Find(car => true).ToEnumerable();
        }

        public Car GetItem(string id)
        {
            return _db.Find(car => car.CarId == id ).FirstOrDefault();
        }

        public void Create(Car item)
        {
            _db.InsertOne(item);
        }

        public void Update(Car item)
        {
            Car finded = _db.Find(car => car.CarId == item.CarId).FirstOrDefault();
            if (finded != null)
            {
                finded.CarName = item.CarName ?? finded.CarName;
                finded.Description = item.Description ?? finded.Description;

                _db.ReplaceOne(car => car.CarId == item.CarId, finded);
            }
        }

        public void Delete(string id)
        {
            _db.DeleteOne(car => car.CarId == id);
        }
    }
}