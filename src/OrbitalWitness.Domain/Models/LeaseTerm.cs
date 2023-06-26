namespace OrbitalWitness.Domain.Models;

public class LeaseTerm
{
    public string Length { get; set; } = "";
    public DateTime StartDate { get; set; }

    public override string ToString() => $"{Length} {StartDate:dd.MM.yyyy}";
}