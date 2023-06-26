using Newtonsoft.Json;

namespace OrbitalWitness.Application.Models;

public class ScheduleEntry
{
    [JsonProperty("entryNumber")]
    public string EntryNumber { get; set; } = "";
    
    [JsonProperty("entryDate")]
    public DateTime? EntryDate { get; set; }

    [JsonProperty("entryType")]
    public string EntryType { get; set; } = "";

    [JsonProperty("entryText")]
    public required List<string> Entries { get; set; }
}