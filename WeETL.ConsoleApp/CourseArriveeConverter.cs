using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeETL.ConsoleApp
{
    public class CourseArriveeConverter : JsonConverter<List<int>>
    {
        public override List<int> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {



            List<int> result = new List<int>();

            while (ReadInt(ref reader, ref result)) { };
            return result;
        }
        private bool ReadInt(ref Utf8JsonReader reader, ref List<int> result)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                reader.Read();
                while (reader.TokenType == JsonTokenType.Number)
                {
                    result.Add(reader.GetInt16());
                    reader.Read();
                }
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    reader.Read();
                }
                return ReadInt(ref reader, ref result);


            }
            return false;
        }
        public override void Write(Utf8JsonWriter writer, List<int> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
