using ServiceBySocket.Extensions;
using ServiceBySocket.Models;

namespace ServiceBySocket.Services
{
    public class CarService
    {
        private readonly WriteConsoleExtend console;
        public CarService()
        {
            console = new WriteConsoleExtend();
        }

        public Cars GetAllCart()
        {
            var result = new Cars();

            using (var dbcontext = new Context())
            {
                try
                {
                    result.ListCars = dbcontext.Сars.ToList();

                }
                catch (Exception ex)
                {
                    console.Error($"Ошибка получения записей. {ex.Message}");
                }
            }

            return result;
        }

        public Car GetCarById(string id)
        {
            Car car = new Car();

            using (var dbcontext = new Context())
            {
                try
                {
                    car = dbcontext.Сars.FirstOrDefault(x => x.Id.ToString().Equals(id));

                }
                catch (Exception ex)
                {
                    console.Error($"Ошибка получения записи по ID. {id}, {ex.Message}");
                    throw;
                }
            }
            return car;
        }

        private Car GetGenericCar()
        {
            var car = new Car
            {
                Id = Guid.NewGuid(),
                Model = "nISSAN",
                EngineCapacity = 1.8F,
                DoorsCount = 0,
                Year = 2050
            };

            return car;
        }


    }
}
