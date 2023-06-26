using WireMock.Server;

namespace OrbitalWitness.Tests.Integration;

public class MockServerFixture : IDisposable
{
    public readonly WireMockServer Server;

    public MockServerFixture()
    {
        Server = WireMockServer.Start();
    }

    public void Dispose()
    {
        Server?.Dispose();
    }
}