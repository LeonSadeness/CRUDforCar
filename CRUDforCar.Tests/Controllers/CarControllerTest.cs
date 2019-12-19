using CRUDforCar.Controllers;
using CRUDforCar.Models;
using CRUDforCar.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CRUDforCar.Tests
{
    public class CarControllerTest
    {
        [Fact]
        public void GetAllCars()
        {
            // Arrange
            List<Car> fakeCollection = new List<Car>
            {
                new Car
                {
                    CarId = "1",
                    CarName = "Volvo",
                    Description = "Надежность"
                },
                new Car
                {
                    CarId = "2",
                    CarName = "BMW",
                    Description = "Скорость"
                },
                new Car
                {
                    CarId = "3",
                    CarName = "UAZ",
                    Description = "Проходимость"
                },
                new Car
                {
                    CarId = "4",
                    CarName = "Belaz",
                    Description = "Грузоподьемность"
                }
            };
            var mock = new MockMethodsCarsDBService(fakeCollection);
            var controller = new CarsController(mock);

            // Act
            var result = controller.Get();

            // Assert
            var viewResult = Assert.IsType<ActionResult<List<Car>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Car>>(viewResult.Value);
            Assert.Equal(4, model.Count());
        }

        [Fact]
        public void GetCarFromId()
        {
            // Arrange
            List<Car> fakeCollection = new List<Car>
            {
                new Car
                {
                    CarId = "2",
                    CarName = "BMW",
                    Description = "Скорость"
                }
            };
            var mock = new MockMethodsCarsDBService(fakeCollection);
            var controller = new CarsController(mock);

            // Act
            var result = controller.Get("2");

            // Assert
            var viewResult = Assert.IsType<ActionResult<Car>>(result);
            Assert.IsAssignableFrom<Car>(viewResult.Value);
            Assert.Equal("BMW", viewResult.Value.CarName);
        }

        [Fact]
        public void CreateCar()
        {
            // Arrange
            var car = new Car
            {
                CarId = "1",
                CarName = "Lada",
                Description = "Доступность"
            };
            var mock = new MockMethodsCarsDBService();
            var controller = new CarsController(mock);

            // Act
            var result = controller.Create(car);

            // Assert
            var viewResult = Assert.IsType<ActionResult<Car>>(result).Result as CreatedAtRouteResult;
            Assert.IsAssignableFrom<Car>(viewResult.Value);
            Assert.Equal(201, viewResult.StatusCode);
        }

        [Fact]
        public void UpdateCar()
        {
            // Arrange
            List<Car> fakeCollection = new List<Car>
            {
                new Car
                {
                    CarId = "1",
                    CarName = "Volvo",
                    Description = "Надежность"
                }
            };
            var mock = new MockMethodsCarsDBService(fakeCollection);
            var controller = new CarsController(mock);

            // Act
            var car = new Car
            {
                Description = "Test"
            };
            var result = controller.Update("1", car);
            var result2 = controller.Get("1");

            // Assert
            var viewResult = Assert.IsAssignableFrom<NoContentResult>(result);
            Assert.Equal(204, viewResult.StatusCode);
            Assert.Equal("Volvo", result2.Value.CarName);
            Assert.Equal("Test", result2.Value.Description);
        }

        [Fact]
        public void DeleteCar()
        {
            // Arrange
            List<Car> fakeCollection = new List<Car>
            {
                new Car
                {
                    CarId = "1",
                    CarName = "Volvo",
                    Description = "Надежность"
                }
            };
            var mock = new MockMethodsCarsDBService(fakeCollection);
            var controller = new CarsController(mock);

            // Act
            var result = controller.Delete("1");
            var result2 = controller.Get("1");

            // Assert
            var viewResult = Assert.IsAssignableFrom<NoContentResult>(result);
            Assert.Equal(204, viewResult.StatusCode);
            var viewResult2 = Assert.IsAssignableFrom<NotFoundResult>(result2.Result);
            Assert.Equal(404, viewResult2.StatusCode);

        }
    }
}
