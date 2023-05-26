using ServiceBySocket.Models;

namespace ServiceBySocket.Services
{
    public class CarService
    {
        public CarService()
        {
        }

        public List<Car> GetAllCart()
        {
            var result = new List<Car>();

            for (int i = 0; i < 10; i++)
            {
                result.Add(GetGenericCar());
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
