using OrbitalWitness.Application.Interfaces;
using OrbitalWitness.Application.Mapping;
using OrbitalWitness.Application.Models;
using OrbitalWitness.Domain.Models;
using Shouldly;

namespace OrbitalWitness.Tests.Unit;

public class LeaseMappingTests
{
    private readonly IMapper<ScheduleEntry, ScheduleOfLease> _mapper;

    public LeaseMappingTests()
    {
        _mapper = new LeaseScheduleMapper();
    }

    [Fact]
    public void When_SourceHasAllRows_Then_ShouldReturnCorrectResult()
    {
        // Arrange
        var source = new ScheduleEntry
        {
            EntryNumber = "1",
            EntryType = "Schedule of Notices of Leases",
            Entries = new List<string>
            {
                "28.01.2009      Transformer Chamber (Ground   23.01.2009      EGL551039  ",
                "tinted blue     Floor)                        99 years from              ",
                "(part of)                                     23.1.2009"
            }
        };

        // Act
        var target = _mapper.Map(source);

        // Assert
        target.ShouldNotBeNull();
        target.LesseesTitle.ShouldBe("EGL551039");
        target.PropertyDescription.ShouldBe("Transformer Chamber (Ground Floor)");
        target.Registration?.RegistrationDate.ShouldBe(new DateTime(2009, 1, 28));
        target.Registration?.PlanReference.ShouldBe("tinted blue (part of)");
        target.Duration?.DateOfLease.ShouldBe(new DateTime(2009, 1, 23));
        target.Duration?.Term?.Length.ShouldBe("99 years from");
        target.Duration?.Term?.StartDate.ShouldBe(new DateTime(2009, 1, 23));
        target.Notes.ShouldBeEmpty();
    }

    [Fact]
    public void When_SourceHasMinimalRows_Then_ShouldReturnCorrectResult()
    {
        // Arrange
        var source = new ScheduleEntry
        {
            EntryNumber = "1",
            EntryType = "Schedule of Notices of Leases",
            Entries = new List<string>
            {
                "05.07.2016      27 Upton Drive                27.05.2016      SF617600   ",
                "35                                            999 years from             ",
                "01.01.2015"
            }
        };

        // Act
        var target = _mapper.Map(source);

        // Assert
        target.ShouldNotBeNull();
        target.LesseesTitle.ShouldBe("SF617600");
        target.PropertyDescription.ShouldBe("27 Upton Drive");
        target.Registration?.RegistrationDate.ShouldBe(new DateTime(2016, 7, 5));
        target.Registration?.PlanReference.ShouldBe("35");
        target.Duration?.DateOfLease.ShouldBe(new DateTime(2016, 5, 27));
        target.Duration?.Term?.Length.ShouldBe("999 years from");
        target.Duration?.Term?.StartDate.ShouldBe(new DateTime(2015, 1, 1));
        target.Notes.ShouldBeEmpty();
    }

    [Fact]
    public void When_SourceHasNotes_Then_ShouldReturnCorrectResult()
    {
        // Arrange
        var source = new ScheduleEntry
        {
            EntryNumber = "1",
            EntryType = "Schedule of Notices of Leases",
            Entries = new List<string>
            {
                "17.06.2016      17 Metcalfe Close (ground,    31.03.2016      SF617229   ",
                "67, 68 & 46     first & second floor flat)    125 years from             ",
                "(part of)                                     01.01.2015                 ",
                "NOTE: As to the parking space only the surface is included in the lease."
            }
        };

        // Act
        var target = _mapper.Map(source);

        // Assert
        target.ShouldNotBeNull();
        target.LesseesTitle.ShouldBe("SF617229");
        target.PropertyDescription.ShouldBe("17 Metcalfe Close (ground, first & second floor flat)");
        target.Registration?.RegistrationDate.ShouldBe(new DateTime(2016, 6, 17));
        target.Registration?.PlanReference.ShouldBe("67, 68 & 46 (part of)");
        target.Duration?.DateOfLease.ShouldBe(new DateTime(2016, 3, 31));
        target.Duration?.Term?.Length.ShouldBe("125 years from");
        target.Duration?.Term?.StartDate.ShouldBe(new DateTime(2015, 1, 1));
        target.Notes.ShouldNotBeNull();
        target.Notes.First().ShouldBe("NOTE: As to the parking space only the surface is included in the lease.");
    }       
}