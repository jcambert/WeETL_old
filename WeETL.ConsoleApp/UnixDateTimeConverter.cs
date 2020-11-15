using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeETL.ConsoleApp
{
    public sealed class UnixDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                DateTime result= DateTime.UnixEpoch.AddMilliseconds(reader.GetDouble()) ;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var val=new DateTimeOffset(value).ToUnixTimeMilliseconds();
            writer.WriteNumberValue(val);
        }
    }
}