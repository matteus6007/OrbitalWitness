using Newtonsoft.Json;
using OrbitalWitness.Application.Interfaces;
using OrbitalWitness.Application.Models;
using OrbitalWitness.Infrastructure;
using Shouldly;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace OrbitalWitness.Tests.Integration;

public class LandRegistryClientTests : IClassFixture<MockServerFixture>
{
    private readonly MockServerFixture _fixture;
    private readonly ILandRegistryClient _sut;

    public LandRegistryClientTests(MockServerFixture fixture)
    {
        _fixture = fixture;

        var client = new HttpClient
        {
            BaseAddress = new Uri(fixture.Server.Urls[0])
        };

        _sut = new LandRegistryClient(client);      
    }

    [Fact]
    public async Task GivenApiReturnsResults_ThenShouldBeMappedCorrectly()
    {
        // Arrange
        var response = ExampleLeaseScheduleResult();

        _fixture.Server
            .Given(Request.Create()
                .WithPath("/leaseschedule")
                .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithBody(response));

        // Act
        var result = await _sut.GetLeaseScheduleAsync();

        // Assert
        result.ShouldNotBeNull();
        result.ShouldNotBeEmpty();
    }

    private static string ExampleLeaseScheduleResult()
    {
        return File.ReadAllText("./Integration/Resources/schedule_of_notices_of_lease_examples.json");
    }
}