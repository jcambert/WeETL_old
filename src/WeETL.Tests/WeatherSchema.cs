using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WeETL.Tests
{
    public class Coordonnee
    {
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }
        [JsonPropertyName("lat")]
        public double  Latitude { get; set; }
        public override string ToString() => $"Lon:{Longitude}-Lat:{Latitude}";
    }
    public class WeatherSchema
    {
        [JsonPropertyName("coord")]
       
        public Coordonnee Coordonnee { get; set; }

        [JsonPropertyName("name")]
        [LogHeader("Lieux")]
        public string Name { get; set; }
    }
}
