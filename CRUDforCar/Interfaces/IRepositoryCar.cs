using CRUDforCar.Models;
using System.Collections.Generic;

namespace CRUDforCar.Interfaces
{
    public interface IRepositoryCar
    {
        IEnumerable<Car> GetItemList();

        Car GetItem(string id);

        void Create(Car item);

        void Update(Car item);

        void Delete(string id);
    }
}