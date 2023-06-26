namespace OrbitalWitness.Domain.Models;

public class LeaseDuration
{
    public DateTime DateOfLease { get; set; }
    public LeaseTerm? Term { get; set; }

    public override string ToString() => $"{DateOfLease:dd.MM.yyyy} {Term}";
}