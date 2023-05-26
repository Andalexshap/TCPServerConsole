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
            var car = new Car();
            var random = new Random();

            car.Year = random.Next();
            car.DoorsCount = random.Next();

            return car;
        }

        
    }
}
