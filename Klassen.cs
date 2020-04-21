using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace Buchungsvorschlaege
{
    public partial class Kalenderexport
    {
        [JsonProperty("@odata.context")]
        public Uri OdataContext { get; set; }

        [JsonProperty("value")]
        public List<Value> Value { get; set; }

        public void ShowAll() //Zeig alle Einträge des Kalenders in der Konsole
        {
            for (int i = 0; i < this.Value.Count; i++)
            {
                Console.WriteLine(this.Value[i].ToString()); //Ausgabe der ToString Methode, für jedes Element
            }
        }
        public static Kalenderexport loadJson() //Liest und konvertiert den JSON Datensatz
        {
            string jsonString = File.ReadAllText(@"..\..\..\json1.json");
            var kal = Kalenderexport.FromJson(jsonString);
            return kal;
        }
    }

    public partial class Value
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("importance")]
        public string Importance { get; set; }

        [JsonProperty("isAllDay")]
        public bool IsAllDay { get; set; }

        [JsonProperty("isCancelled")]
        public bool IsCancelled { get; set; }

        [JsonProperty("showAs")]
        public string ShowAs { get; set; }

        [JsonProperty("start")]
        public End Start { get; set; }

        [JsonProperty("end")]
        public End End { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("recurrence")]
        public Recurrence Recurrence { get; set; }

        public override string ToString() //ToString Methode zur Konsolenausgabe
        {
            return "Bezeichnung (Subject): " + this.Subject + " Startzeit: " + this.Start.DateTime;
        }
    }

    public partial class End
    {
        [JsonProperty("dateTime")]
        public DateTimeOffset DateTime { get; set; }

        [JsonProperty("timeZone")]
        public TimeZone TimeZone { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("locationType")]
        public string LocationType { get; set; }

        [JsonProperty("uniqueId", NullValueHandling = NullValueHandling.Ignore)]
        public string UniqueId { get; set; }

        [JsonProperty("uniqueIdType")]
        public string UniqueIdType { get; set; }

        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)]
        public Address Address { get; set; }

        [JsonProperty("coordinates", NullValueHandling = NullValueHandling.Ignore)]
        public Address Coordinates { get; set; }
    }

    public partial class Address
    {
    }

    public partial class Recurrence
    {
        [JsonProperty("pattern")]
        public Pattern Pattern { get; set; }

        [JsonProperty("range")]
        public Range Range { get; set; }
    }

    public partial class Pattern
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("interval")]
        public long Interval { get; set; }

        [JsonProperty("month")]
        public long Month { get; set; }

        [JsonProperty("dayOfMonth")]
        public long DayOfMonth { get; set; }

        [JsonProperty("daysOfWeek")]
        public List<string> DaysOfWeek { get; set; }

        [JsonProperty("firstDayOfWeek")]
        public string FirstDayOfWeek { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }
    }

    public partial class Range
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("startDate")]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTimeOffset EndDate { get; set; }

        [JsonProperty("recurrenceTimeZone")]
        public string RecurrenceTimeZone { get; set; }

        [JsonProperty("numberOfOccurrences")]
        public long NumberOfOccurrences { get; set; }
    }

    public enum TimeZone { Utc };

    public partial class Kalenderexport
    {
        //Diese Funktion konvertiert (Deserialisiert) das JSON Objekt in C# Instanzen
        public static Kalenderexport FromJson(string json) => JsonConvert.DeserializeObject<Kalenderexport>(json, Buchungsvorschlaege.Converter.Settings);
    }
    //Alles hier drunter ist momentan nicht relevant
    public static class Serialize
    {
        public static string ToJson(this Kalenderexport self) => JsonConvert.SerializeObject(self, Buchungsvorschlaege.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TimeZoneConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class TimeZoneConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TimeZone) || t == typeof(TimeZone?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "UTC")
            {
                return TimeZone.Utc;
            }
            throw new Exception("Cannot unmarshal type TimeZone");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TimeZone)untypedValue;
            if (value == TimeZone.Utc)
            {
                serializer.Serialize(writer, "UTC");
                return;
            }
            throw new Exception("Cannot marshal type TimeZone");
        }

        public static readonly TimeZoneConverter Singleton = new TimeZoneConverter();
    }
}