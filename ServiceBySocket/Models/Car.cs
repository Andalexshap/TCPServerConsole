using System.Runtime.CompilerServices;
using System.Text;

namespace ServiceBySocket.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public float EngineCapacity { get; set; }
        public int DoorsCount { get; set; }

        public override string ToString()
        {
            return  $"Модель: {Model};" +
                    $"Год: {Year};" +
                    $"Объем: {EngineCapacity};" +
                    $"Кол-во дверей: {DoorsCount};";
        }

        public string ConvertToHexForSend() => "0x02" + CountPropertiesNotNull();


        private string CountPropertiesNotNull()
        {
            var message = string.Empty;
            var model = string.Empty;
            var year = string.Empty;
            var engineCapacity = string.Empty;
            var doorsCount = string.Empty;

            var countProperties = 0;

            if (!String.IsNullOrEmpty(Model))
            {
                model = ModelToHex();
                countProperties++;
            }

            if(Year != default)
            {
                year = YearToHex();
                countProperties++;
            }

            if(EngineCapacity != default)
            {
                engineCapacity = EngineCapacityToHex();
                countProperties++;
            }

            if (DoorsCount != default)
            {
                doorsCount = DoorsCountToHex();
                countProperties++;
            }

            if (countProperties == 0) return string.Empty;

            message += " 0x" + countProperties + model + year + engineCapacity + doorsCount;

            return message;
        }

        #region ConvertToHex
        private string ModelToHex()
        {
            byte[] data = Encoding.Default.GetBytes(Model);
            string result = $" 0x09 0x0{data.Length}";
            
            foreach (byte b in data)
            {
                result += " 0x" + b.ToString("X2");
            }

            return result;
        }

        private string YearToHex()
        {
            var dataYear = Convert.ToString(Year, 16);
            var result = " 0x12 ";

            result += "0x0" + dataYear[0];
            result += " 0x" + dataYear[1] + dataYear[2];

            return result;
        }

        private string EngineCapacityToHex()
        {
            var result = " 0x13";
            byte[] bytes = BitConverter.GetBytes(EngineCapacity);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            foreach (byte b in bytes)
            {
                result += " 0x" + b.ToString("X2");
            }

            return result;
        }

        private string DoorsCountToHex() => " 0x12 0x0" + Convert.ToString(DoorsCount, 16);
#endregion
    }
}
