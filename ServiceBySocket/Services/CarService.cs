using ServiceBySocket.Models;

namespace ServiceBySocket.Services
{
    public class CarService
    {
        public CarService()
        {
        }

        public Cars GetAllCars()
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
                    Console.WriteLine($"Ошибка получения записей. {ex}");
                    throw;
                }
            }

            return result;
        }

        public async Task<Cars> GetCarById(string id)
        {
            var cars = new Cars();
            Car car = new Car();

            using (var dbcontext = new Context())
            {
                try
                {
                    car = dbcontext.Сars.FirstOrDefault(x => x.Id.ToString().Equals(id));

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка получения записи по ID. {id}, {ex}");
                    throw;
                }

                cars.ListCars = new List<Car> { car };
            }
            return cars;
        }
    }
}
