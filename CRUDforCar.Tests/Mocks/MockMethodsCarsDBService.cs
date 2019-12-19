using System.Collections.Generic;
using CRUDforCar.Interfaces;
using CRUDforCar.Models;

namespace CRUDforCar.Tests.Mocks
{
    class MockMethodsCarsDBService : IRepositoryCar
    {
        private List<Car> _fakeList { get; set; }

        public MockMethodsCarsDBService(List<Car> fakeList = null)
        {
            _fakeList = new List<Car>();
            if (fakeList != null)
                _fakeList = fakeList;
        }

        public IEnumerable<Car> GetItemList()
        {
            return _fakeList;
        }

        public Car GetItem(string id)
        {
            return _fakeList.Find(car => car.CarId == id);
        }

        public void Create(Car item)
        {
            _fakeList.Add(item);
        }

        public void Update(Car item)
        {
            var finded = _fakeList.Find(car => car.CarId == item.CarId);
            if (finded != null)
            {
                finded.CarName = item.CarName ?? finded.CarName;
                finded.Description = item.Description ?? finded.Description;
            }
        }

        public void Delete(string id)
        {
            var finded = _fakeList.Find(car => car.CarId == id);
            if (finded != null)
                _fakeList.Remove(finded);
        }
    }
}