using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WeETL.Databases;
using WeETL.Databases.MongoDb;

namespace WeETL.ConsoleApp
{
    public partial class WeatherSchema: DocumentByObjectId
    {
    }
    public class Coordonnee
    {
        [JsonPropertyName("lon")]
        public double Longitude { get; set; }
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }
        public override string ToString() => $"Lon:{Longitude}-Lat:{Latitude}";
    }
    public class Weather
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("icon")]
        public string Icon { get; set; }


    }

    public class Temperature
    {
        [JsonPropertyName("temp")]
        public double Nominale { get; set; }
        [JsonPropertyName("feels_like")]
        public double Feel { get; set; }
        [JsonPropertyName("temp_min")]
        public double Minimum { get; set; }
        [JsonPropertyName("temp_max")]
        public double Maximum { get; set; }
        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    public class Wind
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }
        [JsonPropertyName("deg")]
        public int Direction { get; set; }
    }
    public partial class WeatherSchema
    {
        [JsonPropertyName("coord")]
        public Coordonnee Coordonnee { get; set; }

        [JsonPropertyName("name")]
        [LogHeader("Lieux")]
        public string Name { get; set; }

        [JsonPropertyName("weather")]
        public List<Weather> Weathers { get; set; }

        [JsonPropertyName("main")]
        public Temperature Temperature { get; set; }

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }
    }
}
