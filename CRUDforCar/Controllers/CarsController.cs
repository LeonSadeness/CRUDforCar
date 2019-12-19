using CRUDforCar.Interfaces;
using CRUDforCar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CRUDforCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IRepositoryCar _carService;

        public CarsController(IRepositoryCar carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public ActionResult<List<Car>> Get()
        {
            return _carService.GetItemList().ToList();
        }

        [HttpGet("{id:length(0,24)}", Name = "GetCar")]
        public ActionResult<Car> Get(string id)
        {
            var car = _carService.GetItem(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPost]
        public ActionResult<Car> Create(Car car)
        {
            _carService.Create(car);

            return CreatedAtRoute("GetCar", new { id = car.CarId }, car);
        }

        [HttpPut("{id:length(0,24)}")]
        public IActionResult Update(string id, Car carIn)
        {
            var car = _carService.GetItem(id);

            if (car == null)
            {
                return NotFound();
            }

            carIn.CarId = id;
            _carService.Update(carIn);

            return NoContent();
        }

        [HttpDelete("{id:length(0,24)}")]
        public IActionResult Delete(string id)
        {
            var car = _carService.GetItem(id);

            if (car == null)
            {
                return NotFound();
            }

            _carService.Delete(id);

            return NoContent();
        }
    }
}
