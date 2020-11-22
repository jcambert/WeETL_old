using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WeETL.Databases;

namespace WeETL.ConsoleApp
{
    /// <summary>
    /// @see https://github.com/florianzemma/CoronavirusAPI-France
    /// </summary>
    public class AllLiveFranceData 
    {

        [JsonPropertyName("allLiveFranceData")]
        public List<Covid19Schema> Covid19LiveDataSchema { get; set; }

        [JsonPropertyName("allFranceDataByDate")]
        public List<Covid19Schema> AllFranceDataByDate { get; set; }
    }
    public class Covid19Schema : DocumentByObjectId
    {
        [JsonPropertyName("code"), BsonElement("code")]
        public string Code { get; set; }
        [JsonPropertyName("nom"), BsonElement("nom")]
        public string Nom { get; set; }
        [JsonPropertyName("date"), BsonElement("date")]
        public string Date { get; set; }
        [JsonPropertyName("hospitalises"), BsonElement("hospitalises")]
        public int Hospitalises { get; set; }
        [JsonPropertyName("reanimation"), BsonElement("reanimation")]
        public int Reanimation { get; set; }
        [JsonPropertyName("nouvellesHospitalisations"), BsonElement("nouv_hospi")]
        public int NouvellesHospitalisations { get; set; }
        [JsonPropertyName("nouvellesReanimations"), BsonElement("nouv_rea")]
        public int NouvellesReanimations { get; set; }
        [JsonPropertyName("deces"), BsonElement("deces")]
        public int Deces { get; set; }
        [JsonPropertyName("gueris"), BsonElement("hueris")]
        public int Gueris { get; set; }
    }
}
