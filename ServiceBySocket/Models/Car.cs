using System.Runtime.CompilerServices;
using System.Text;

namespace ServiceBySocket.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double EngineCapacity { get; set; }
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
            var engineCapacity = BitConverter.DoubleToInt64Bits(EngineCapacity).ToString("X");

            string result = $" 0x13 0x3F";

            foreach (byte b in engineCapacity)
            {
                result += " 0x" + b.ToString("X2");
            }
            Console.WriteLine(result);

            return " 0x0" + engineCapacity[0] + " 0x" + engineCapacity[1] + engineCapacity[2];            
        }

        private string DoorsCountToHex() => " 0x12 0x0" + Convert.ToString(DoorsCount, 16);
#endregion
    }
}
