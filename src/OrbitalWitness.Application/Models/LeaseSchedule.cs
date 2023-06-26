using Newtonsoft.Json;

namespace OrbitalWitness.Application.Models;

public class LeaseSchedule
{
    [JsonProperty("leaseschedule")]
    public required LeaseScheduleDetails Details {get; set;}
}