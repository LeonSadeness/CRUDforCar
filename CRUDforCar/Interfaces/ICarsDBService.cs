using CRUDforCar.Models;
using System.Collections.Generic;

namespace CRUDforCar.Interfaces
{
    public interface ICarsDBService
    {
        Car Create(Car car);

        List<Car> Get();

        Car Get(string id);

        void Update(string id, Car carIn);

        void Remove(Car carIn);

        void Remove(string id);
    }
}