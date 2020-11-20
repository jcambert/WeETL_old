using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using WeETL.Databases;

namespace WeETL.Schemas
{

    public partial class OpenWeatherMapSchema : DocumentByObjectId
    {
    }
    public  class Weather
    {
        [JsonPropertyName("id"), BsonElement("id")]
        public int Id { get; set; }
        [JsonPropertyName("description"), BsonElement("description")]
        public string Description { get; set; }
        [JsonPropertyName("icon"), BsonElement("icon")]
        public string Icon { get; set; }


    }
    public class Coordonnee
    {
        [JsonPropertyName("lon"), BsonElement("lon")]
        public double Longitude { get; set; }
        [JsonPropertyName("lat"), BsonElement("lat")]
        public double Latitude { get; set; }
        public override string ToString() => $"Lon:{Longitude}-Lat:{Latitude}";
    }

    public class Temperature
    {
        [JsonPropertyName("temp"), BsonElement("temp")]
        public double Nominale { get; set; }
        [JsonPropertyName("feels_like"), BsonElement("feels_like")]
        public double Feel { get; set; }
        [JsonPropertyName("temp_min"), BsonElement("temp_min")]
        public double Minimum { get; set; }
        [JsonPropertyName("temp_max"), BsonElement("temp_max")]
        public double Maximum { get; set; }
        [JsonPropertyName("pressure"), BsonElement("pressure")]
        public int Pressure { get; set; }
        [JsonPropertyName("humidity"), BsonElement("humidity")]
        public int Humidity { get; set; }
    }

    public class Wind
    {
        [JsonPropertyName("speed"), BsonElement("speed")]
        public double Speed { get; set; }
        [JsonPropertyName("deg"), BsonElement("deg")]
        public int Direction { get; set; }
    }
    public partial class OpenWeatherMapSchema
    {
        [JsonPropertyName("coord"), BsonElement("coord")]
        public Coordonnee Coordonnee { get; set; }

        [JsonPropertyName("name"), BsonElement("name")]
        [LogHeader("Lieux")]
        public string Name { get; set; }

        [JsonPropertyName("weather"), BsonElement("weather")]
        public List<Weather> Weathers { get; set; }

        [JsonPropertyName("main"), BsonElement("main")]
        public Temperature Temperature { get; set; }

        [JsonPropertyName("wind"),BsonElement("wind")]
        public Wind Wind { get; set; }
    }
}
