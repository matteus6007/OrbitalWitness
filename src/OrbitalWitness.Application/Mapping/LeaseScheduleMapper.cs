using System.Text.RegularExpressions;
using OrbitalWitness.Application.Interfaces;
using OrbitalWitness.Application.Models;
using OrbitalWitness.Domain.Models;

namespace OrbitalWitness.Application.Mapping;

public class LeaseScheduleMapper : IMapper<ScheduleEntry, ScheduleOfLease>
{
    private readonly Regex _delimeterPattern = new("[\\s]{2,}", RegexOptions.Compiled);
    private const string Delimeter = "¬";

    public ScheduleOfLease Map(ScheduleEntry data)
    {
        // define columns
        var registrationRows = new List<string>();
        var propertyDescriptionRows = new List<string>();
        var leaseDurationRows = new List<string>();
        var lesseesTitleRows = new List<string>();
        var notesRows = new List<string>();

        // loop entries
        for (var i = 0; data.Entries.Count > i; i++)
        {
            // map entry
            var entry = _delimeterPattern.Replace(data.Entries[i].Trim(), Delimeter);

            // split into columns
            var columns = entry.Split(Delimeter);

            // validate columns
            if (columns.Length == 1)
            {
                // lease requires 3 entries
                if (i < 3)
                {
                    leaseDurationRows.Add(columns[0]);
                }
                else
                {
                    if (entry.Contains("note", StringComparison.InvariantCultureIgnoreCase))
                    {
                        notesRows.Add(entry);
                    }
                    else
                    {
                        // TODO: work out whether the column is registration
                        // or property description
                        
                        // registration requires minimum 2 entries
                        registrationRows.Add(columns[0]);                        
                    }
                }                
            }

            if (columns.Length == 2)
            {
                // TODO: work out whether the first column is registration
                // or property description
                 
                // registration requires minimum 2 entries
                registrationRows.Add(columns[0]);

                // lease requires 3 entries
                if (i < 3)
                {
                    leaseDurationRows.Add(columns[1]);
                }

                continue;
            }

            if (columns.Length == 3)
            {
                // lessees's title only has 1 entry
                registrationRows.Add(columns[0]);
                propertyDescriptionRows.Add(columns[1]);
                leaseDurationRows.Add(columns[2]);

                continue;
            }

            if (columns.Length == 4)
            {
                registrationRows.Add(columns[0]);
                propertyDescriptionRows.Add(columns[1]);
                leaseDurationRows.Add(columns[2]);
                lesseesTitleRows.Add(columns[3]);
            }
        }

        var result = new ScheduleOfLease
        {
            Registration = MapLeaseRegistration(registrationRows),
            PropertyDescription = string.Join(" ", propertyDescriptionRows),
            Duration = MapLeaseDuration(leaseDurationRows),
            LesseesTitle = string.Join(" ", lesseesTitleRows),
            Notes = notesRows
        };

        return result;
    }

    private LeaseRegistration MapLeaseRegistration(List<string> rows)
    {
        // TODO: do something if cannot be parsed
        _ = DateTime.TryParse(rows[0], out var registrationDate);

        return new LeaseRegistration
        {
            RegistrationDate = registrationDate,
            PlanReference = string.Join(" ", rows.Select(x => x).Skip(1))
        };
    }

    private LeaseDuration MapLeaseDuration(List<string> rows)
    {
        // TODO: do something if cannot be parsed
        _ = DateTime.TryParse(rows[0], out var dateOfLease);

        DateTime startDate = dateOfLease;

        if (rows.Count == 3)
        {
            _ = DateTime.TryParse(rows[2], out startDate);
        }

        return new LeaseDuration
        {
            DateOfLease = dateOfLease,
            Term = new LeaseTerm
            {
                Length = rows[1],
                StartDate = startDate
            }
        };
    }
}
