using System.Security.Cryptography.X509Certificates;
using ServiceBySocket.Helpers;

namespace ServiceBySocket.Models
{
    public class Cars
    {
        public List<Car> ListCars { get; set; }

        public override string ToString() => String.Join("|", ListCars);
        public List<byte> ConvertCarsToByteForSend()
        {
            var bytes = new List<byte>();
            foreach (var car in ListCars)
            {
                bytes.AddRange(car.ConvertCarToByteForSend());
            }
            return bytes;
        } 
       
    }
}
