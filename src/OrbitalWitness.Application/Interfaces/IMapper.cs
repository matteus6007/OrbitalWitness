namespace OrbitalWitness.Application.Interfaces;

public interface IMapper<in TSource, out TDest>
{
    TDest Map(TSource data);
}