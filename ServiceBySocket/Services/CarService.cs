using ServiceBySocket.Models;

namespace ServiceBySocket.Services
{
    public class CarService
    {
        public CarService()
        {
        }

        public Cars GetAllCart()
        {
            var result = new Cars { ListCars = new List<Car>() };

            for (int i = 0; i < 10; i++)
            {
                result.ListCars.Add(GetGenericCar());
            }

            return result;
        }

        public Car GetCarById(string id)
        {
            return GetGenericCar();
        }

        private Car GetGenericCar()
        {
            var car = new Car
            {
                Id = Guid.NewGuid(),
                Manufacturer = "NISSAN",
                Model = "nISSAN",
                EngineCapacity = 1.8M,
                DoorsCount = 0,
                Year = 2050
            };

            return car;
        }


    }
}
