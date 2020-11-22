using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;
using WeETL.Databases;

namespace WeETL.ConsoleApp
{
    public class ProgrammeSchema
    {
        [JsonPropertyName("programme")]
        public Programme Programme { get; set; }
    }
    public class Programme : DocumentByObjectId
    {
        [JsonPropertyName("date"),JsonConverter(typeof(UnixDateTimeConverter)),BsonElement("date")]
        public DateTime  Date{ get; set; }
        [JsonPropertyName("reunions"), BsonElement("reunions"),LogIgnore]
        public List<Reunion> Reunions { get; set; }
    }
    [DebuggerDisplay("{NumeroOfficiel}-{Hippodrome}")]
    public class Reunion
    {
        [JsonPropertyName("numOfficiel"), BsonElement("numOfficiel")]
        public int NumeroOfficiel { get; set; }

        [JsonPropertyName("hippodrome"), BsonElement("hippodrome")]
        public Hippodrome Hippodrome { get; set; }
        [JsonPropertyName("pays"), BsonElement("pays")] 
        public Pays Pays { get; set; }
        [JsonPropertyName("courses"), BsonElement("courses")]
        public List<Course> Courses { get; set; }
    }

    [DebuggerDisplay("{LibelleCourt}")]
    public class Hippodrome
    {
        [JsonPropertyName("code"), BsonElement("code")]
        public string Code { get; set; }
        [JsonPropertyName("libelleCourt"), BsonElement("libelleCourt")]
        public string LibelleCourt { get; set; }
        [JsonPropertyName("libelleLong"), BsonElement("libelleLong")]
        public string LibelleLong { get; set; }
    }
    [DebuggerDisplay("{Libelle}")]
    public class Pays
    {
        [JsonPropertyName("code"), BsonElement("code")]
        public string Code { get; set; }
        [JsonPropertyName("libelle"), BsonElement("libelle")]
        public string Libelle { get; set; }
    }
    [DebuggerDisplay("{Numero}-{Libelle}-{string.Join('-',Arrivee)}")]
    public class Course
    {
        [JsonPropertyName("numReunion"), BsonElement("reunion")]
        public int Reunion { get; set; }
        [JsonPropertyName("numOrdre"), BsonElement("numOrdre")]
        public int Numero { get; set; }
        [JsonPropertyName("distance"), BsonElement("distance")]
        public int Distance { get; set; }
        [JsonPropertyName("discipline"), BsonElement("discipline")]
        public string Discipline { get; set; }
        [JsonPropertyName("specialite"), BsonElement("specialite")]
        public string Specialite { get; set; }
        [JsonPropertyName("libelle"), BsonElement("libelle")]
        public string Libelle { get; set; }
        [JsonPropertyName("libelleCourt"), BsonElement("libelleCourt")]
        public string LibelleCourt { get; set; }
        [ BsonElement("participants")]
        public List<Participant> Participants { get; set; } = new List<Participant>();
        [JsonPropertyName("ordreArrivee"),JsonConverter(typeof(CourseArriveeConverter)), BsonElement("arrivee")]
        public List<int> Arrivee { get; set; }

    }

    public class ListeParticipants
    {
        [JsonPropertyName("participants")]
        public List<Participant> Participants { get; set; }
    }
    [DebuggerDisplay("{Numero}-{Nom}")]
    public class Participant
    {
        [JsonPropertyName("nom"), BsonElement("nom")]
        public string Nom { get; set; }
        [JsonPropertyName("numPmu"), BsonElement("numpmu")]
        public int Numero { get; set; }
        [JsonPropertyName("age"), BsonElement("age")]
        public int Age { get; set; }
        [JsonPropertyName("sexe"), BsonElement("sexe")]
        public string Sexe { get; set; }
        [JsonPropertyName("oeilleres"), BsonElement("oeilleres")]
        public string Oeilleres { get; set; }
        [JsonPropertyName("driver"), BsonElement("driver")]
        public string Driver { get; set; }
        [JsonPropertyName("nombreCourses"), BsonElement("nbCourse")]
        public int NombreDeCourses { get; set; }
        [JsonPropertyName("nombreVictoires"), BsonElement("nbVictoire")]
        public int NombreVictoires { get; set; }
        [JsonPropertyName("nombrePlaces"), BsonElement("nbPlace")]
        public int NombrePlaces { get; set; }
        [JsonPropertyName("musique"), BsonElement("musique")]
        public string Musique { get; set; }

    }

}
