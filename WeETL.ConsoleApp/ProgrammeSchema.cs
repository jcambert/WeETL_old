using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;

namespace WeETL.ConsoleApp
{
    public class ProgrammeSchema
    {
        [JsonPropertyName("programme")]
        public Programme Programme { get; set; }
    }
    public class Programme
    {
        [JsonPropertyName("date"),JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime  Date{ get; set; }
        [JsonPropertyName("reunions")]
        public List<Reunion> Reunions { get; set; }
    }
    [DebuggerDisplay("{NumeroOfficiel}-{Hippodrome}")]
    public class Reunion
    {
        [JsonPropertyName("numOfficiel")]
        public int NumeroOfficiel { get; set; }

        [JsonPropertyName("hippodrome")]
        public Hippodrome Hippodrome { get; set; }
        [JsonPropertyName("pays")] 
        public Pays Pays { get; set; }
        [JsonPropertyName("courses")]
        public List<Course> Courses { get; set; }
    }

    [DebuggerDisplay("{LibelleCourt}")]
    public class Hippodrome
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("libelleCourt")]
        public string LibelleCourt { get; set; }
        [JsonPropertyName("libelleLong")]
        public string LibelleLong { get; set; }
    }
    [DebuggerDisplay("{Libelle}")]
    public class Pays
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("libelle")]
        public string Libelle { get; set; }
    }
    [DebuggerDisplay("{Numero}-{Libelle}-{string.Join('-',Arrivee)}")]
    public class Course
    {
        [JsonPropertyName("numReunion")]
        public int Reunion { get; set; }
        [JsonPropertyName("numOrdre")]
        public int Numero { get; set; }
        [JsonPropertyName("distance")]
        public int Distance { get; set; }
        [JsonPropertyName("discipline")]
        public string Discipline { get; set; }
        [JsonPropertyName("specialite")]
        public string Specialite { get; set; }
        [JsonPropertyName("libelle")]
        public string Libelle { get; set; }
        [JsonPropertyName("libelleCourt")]
        public string LibelleCourt { get; set; }
        public List<Participant> Participants { get; set; } = new List<Participant>();
        [JsonPropertyName("ordreArrivee"),JsonConverter(typeof(CourseArriveeConverter))]
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
        [JsonPropertyName("nom")]
        public string Nom { get; set; }
        [JsonPropertyName("numPmu")]
        public int Numero { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }
        [JsonPropertyName("sexe")]
        public string Sexe { get; set; }
        [JsonPropertyName("oeilleres")]
        public string Oeilleres { get; set; }
        [JsonPropertyName("driver")]
        public string Driver { get; set; }
        [JsonPropertyName("nombreCourses")]
        public int NombreDeCourses { get; set; }
        [JsonPropertyName("nombreVictoires")]
        public int NombreVictoires { get; set; }
        [JsonPropertyName("nombrePlaces")]
        public int NombrePlaces { get; set; }
        [JsonPropertyName("musique")]
        public string Musique { get; set; }

    }

}
