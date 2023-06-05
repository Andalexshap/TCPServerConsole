using ServiceBySocket.Helpers;

namespace ServiceBySocket.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public ushort Year { get; set; }
        public float EngineCapacity { get; set; }
        public ushort DoorsCount { get; set; }

        public override string ToString()
        {
            return $"Модель: {Model};" +
                    $"Год: {Year};" +
                    $"Объем: {EngineCapacity};" +
                    $"Кол-во дверей: {DoorsCount};";
        }

        public List<byte> ConvertCarToByteForSend()
            => BinarySerializer.Serialize(this);
    }
}
