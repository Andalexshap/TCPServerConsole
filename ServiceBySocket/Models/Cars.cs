namespace ServiceBySocket.Models
{
    public class Cars
    {
        public List<Car> ListCars { get; set; }

        public override string ToString() => String.Join("|", ListCars);
    }
}
