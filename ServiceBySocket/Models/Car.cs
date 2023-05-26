namespace ServiceBySocket.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal EngineCapacity { get; set; }
        public int DoorsCount { get; set; }

        public override string ToString()
        {
            return $"Производитель: {Manufacturer};" +
                  $"Модель: {Model};" +
                  $"Год: {Year};" +
                  $"Объем: {EngineCapacity};" +
                  $"Кол-во дверей: {DoorsCount};";
        }
    }
}
