using Newtonsoft.Json;

namespace OrbitalWitness.Application.Models;

public class LeaseScheduleDetails
{
    [JsonProperty("scheduleType")]
    public string ScheduleType { get; set; } = "";

    [JsonProperty("scheduleEntry")]
    public List<ScheduleEntry> ScheduleEntries { get; set; }

}