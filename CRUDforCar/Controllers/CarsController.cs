using CRUDforCar.Interfaces;
using CRUDforCar.Models;
using CRUDforCar.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CRUDforCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarsDBService _carService;

        public CarsController(ICarsDBService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public ActionResult<List<Car>> Get() =>
            _carService.Get();

        [HttpGet("{id:length(0,24)}", Name = "GetCar")]
        public ActionResult<Car> Get(string id)
        {
            var car = _carService.Get(id);

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

            return CreatedAtRoute("GetCar", new { id = car.CarId.ToString() }, car);
        }

        [HttpPut("{id:length(0,24)}")]
        public IActionResult Update(string id, Car carIn)
        {
            var car = _carService.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            _carService.Update(id, carIn);

            return NoContent();
        }

        [HttpDelete("{id:length(0,24)}")]
        public IActionResult Delete(string id)
        {
            var car = _carService.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            _carService.Remove(car.CarId);

            return NoContent();
        }
    }
}
