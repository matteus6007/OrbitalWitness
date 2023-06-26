namespace OrbitalWitness.Domain.Models;

public class ScheduleOfLease
{
    public LeaseRegistration? Registration { get; set; }
    public required string PropertyDescription { get; set; }
    public LeaseDuration? Duration { get; set; }
    public required string LesseesTitle { get; set; }
    public List<string> Notes { get; set; }
}