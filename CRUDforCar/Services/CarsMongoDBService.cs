using CRUDforCar.Interfaces;
using CRUDforCar.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace CRUDforCar.Services
{
    public class CarsMongoDBService : ICarsDBService
    {
        private readonly IMongoCollection<Car> _db;

        public CarsMongoDBService(ICarsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _db = database.GetCollection<Car>(settings.ItemCollectionName);
        }

        #region Create

        public Car Create(Car car)
        {
            _db.InsertOne(car);
            return car;
        }

        #endregion

        #region Read

        public List<Car> Get() =>
            _db.Find(car => true).ToList();

        public Car Get(string id) =>
            _db.Find<Car>(car => car.CarId == id).FirstOrDefault();

        #endregion

        #region Update

        public void Update(string id, Car carIn)
        {
            Car finded = _db.Find<Car>(car => car.CarId == id).FirstOrDefault();
            if (finded != null)
            {
                finded.CarName = carIn.CarName ?? finded.CarName;
                finded.Description = carIn.Description ?? finded.Description;

                _db.ReplaceOne(car => car.CarId == id, finded);
            }
        }

        #endregion

        #region Delete

        public void Remove(Car carIn) =>
            _db.DeleteOne(car => car.CarId == carIn.CarId);

        public void Remove(string id) =>
            _db.DeleteOne(car => car.CarId == id);

        #endregion
    }
}