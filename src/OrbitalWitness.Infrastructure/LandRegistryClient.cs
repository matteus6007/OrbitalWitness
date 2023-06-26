using Newtonsoft.Json;
using OrbitalWitness.Application.Interfaces;
using OrbitalWitness.Application.Models;

namespace OrbitalWitness.Infrastructure;

public class LandRegistryClient : ILandRegistryClient
{
    private readonly HttpClient _client;

    public LandRegistryClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<LeaseSchedule>> GetLeaseScheduleAsync()
    {
        // TODO: add authentication
        var request = new HttpRequestMessage(HttpMethod.Get, "leaseschedule");

        var response = await _client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            // TODO: return ErrorOr
        }

        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(content))
        {
            // TODO: return ErrorOr
        }

        return JsonConvert.DeserializeObject<List<LeaseSchedule>>(content);
    }
}
