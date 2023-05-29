using System.Security.Cryptography.X509Certificates;

namespace ServiceBySocket.Models
{
    public class Cars
    {
        public List<Car> ListCars { get; set; }

        public override string ToString() => String.Join("|", ListCars);
        public string ConvertToHexForSend() => String.Join(" ", ListCars.Select(x => x.ConvertToHexForSend()));
       
    }
}
