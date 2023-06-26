using OrbitalWitness.Application.Models;

namespace OrbitalWitness.Application.Interfaces;

public interface ILandRegistryClient
{
    // TODO: Add parameter to get specific entry
    Task<List<LeaseSchedule>> GetLeaseScheduleAsync();
}