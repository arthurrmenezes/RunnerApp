using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;

public class Training
{
    public IdValueObject Id { get; private set; }
    public LocationType Location { get; private set; }
    public double Distance { get; private set; }
    public TimeSpan Duration { get; private set; }
    public DateTime Date { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private const double MaxDistanceKm = 300;
    private static readonly TimeSpan MinDuration = TimeSpan.FromSeconds(5);
    private static readonly TimeSpan MaxDuration = TimeSpan.FromHours(24);

    private Training(LocationType location, double distance, TimeSpan duration, DateTime date)
    {
        Id = IdValueObject.New();
        ValidateDomain(location, distance, duration, date);

        Location = location;
        Distance = distance;
        Duration = duration;
        Date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        CreatedAt = DateTime.UtcNow;
    }

    public static Training Factory(LocationType location, double distance, TimeSpan duration, DateTime date)
        => new Training(location, distance, duration, date);

    public void UpdateTrainingDetails(LocationType location, double distance, TimeSpan duration, DateTime date)
    {
        ValidateDomain(location, distance, duration, date);
        
        Location = location;
        Distance = distance;      
        Duration = duration;
        Date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
    }
    
    private void ValidateDomain(LocationType location, double distance, TimeSpan duration, DateTime date)
    {
        if (!Enum.IsDefined(typeof(LocationType), location))
            throw new ArgumentException("Invalid location type.", nameof(location));

        if (distance <= 0)
            throw new ArgumentException("Distance must be greater than zero.", nameof(distance));
        else if (distance > MaxDistanceKm)
            throw new ArgumentException("Distance must be less than or equal to " + MaxDistanceKm + " km.", nameof(distance));

        if (duration < MinDuration)
            throw new ArgumentException("Duration must be at least " + MinDuration.TotalSeconds + " seconds.", nameof(duration));
        if (duration > MaxDuration)
            throw new ArgumentException("Duration must be less than or equal to " + MaxDuration.TotalHours + " hours.", nameof(duration));

        var dateUtc = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        if (dateUtc > DateTime.UtcNow.AddMinutes(5))
            throw new ArgumentException("Training date cannot be in the future.", nameof(date));
    }
}
