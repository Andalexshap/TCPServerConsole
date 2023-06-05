using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ServiceBySocket.Models;

namespace ServiceBySocket.Helpers
{
    public static class BinarySerializer
    {
        public static List<byte> Serialize(Car car)
        {
            List<byte> bytes = new();
            bytes.Add(0x02);
            PropertyInfo[] properties = car.GetType().GetProperties();

            var counterProperties = 0;
            if (car.Model != null || string.IsNullOrEmpty(car.Model)) counterProperties++;
            if (car.Year != 0) counterProperties++;
            if (car.EngineCapacity != 0) counterProperties++;
            if (car.DoorsCount != 0) counterProperties++;

            //TODO: Переделать получение количества свойств
            bytes.Add((byte)counterProperties);

            foreach (var prop in properties)
            {
                switch (prop.GetValue(car))
                {
                    case string text:
                        WriteString(text, bytes);
                        break;
                    case ushort uint16:
                        WriteUInt16(uint16, bytes);
                        break;
                    case float single:
                        WriteSingle(single, bytes);
                        break;
                    default:
                        break;
                }
            }
            return bytes;
        }

        private static void WriteString(string text, List<byte> result)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            result.Add(0x09);
            result.Add((byte)bytes.Length);
            result.AddRange(bytes);
        }

        private static void WriteSingle(float number, List<byte> result)
        {
            result.Add(0x13);
            byte[] buffer = new byte[sizeof(float)];
            BinaryPrimitives.WriteSingleBigEndian(buffer, number);
            result.AddRange(buffer);
        }

        private static void WriteUInt16(ushort number, List<byte> result)
        {
            result.Add(0x12);
            byte[] buffer = new byte[sizeof(ushort)];
            BinaryPrimitives.WriteUInt16BigEndian(buffer, number);
            result.AddRange(buffer);
        }
    }
}
