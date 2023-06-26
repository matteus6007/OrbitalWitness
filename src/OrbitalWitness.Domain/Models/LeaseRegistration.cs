namespace OrbitalWitness.Domain.Models;

public class LeaseRegistration
{
    public DateTime RegistrationDate { get; set; }
    public required string PlanReference { get; set; }

    public override string ToString() => $"{RegistrationDate:dd.MM.yyyy} {PlanReference}";
}