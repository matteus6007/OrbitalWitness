// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using OrbitalWitness.Application.Mapping;
using OrbitalWitness.Application.Models;

// TODO: get from URL via ILandRegistryClient
const string json = @"
    [{
        ""leaseschedule"": {
            ""scheduleType"": ""SCHEDULE OF NOTICES OF LEASE"",
            ""scheduleEntry"": [
                {
                    ""entryNumber"": ""1"",
                    ""entryDate"": """",
                    ""entryType"": ""Schedule of Notices of Leases"",
                    ""entryText"": [
                        ""28.01.2009      Transformer Chamber (Ground   23.01.2009      EGL551039  "",
                        ""tinted blue     Floor)                        99 years from              "",
                        ""(part of)                                     23.1.2009""
                    ]
                },
                {
                    ""entryNumber"": ""2"",
                    ""entryDate"": """",
                    ""entryType"": ""Schedule of Notices of Leases"",
                    ""entryText"": [
                        ""09.07.2009      Endeavour House, 47 Cuba      06.07.2009      EGL557357  "",
                        ""Edged and       Street, London                125 years from             "",
                        ""numbered 2 in                                 1.1.2009                   "",
                        ""blue (part of)""
                    ]
                }
            ]    
        }
    }]";

var schedules =  JsonConvert.DeserializeObject<List<LeaseSchedule>>(json);

if (schedules.Count == 0)
{
    Console.WriteLine("No schedules found");

    return;
}

var mapper = new LeaseScheduleMapper();

// TODO: handle more than the first schedule
var mappedResults = schedules?.First().Details.ScheduleEntries.Select(mapper.Map);

// TODO: create class to print details
foreach (var schedule in mappedResults)
{
    Console.WriteLine($"Registration date and plan ref: {schedule.Registration}");
    Console.WriteLine($"Property description: {schedule.PropertyDescription}");
    Console.WriteLine($"Date of lease and term: {schedule.Duration}");
    Console.WriteLine($"Lessee’s title: {schedule.LesseesTitle}");

    if (schedule.Notes is not null)
    {
        for (var i = 0; schedule.Notes.Count > i; i++)
        {
            var note = schedule.Notes[i];
            
            Console.WriteLine($"Note {i+1}: {note}");
        }
    }
}