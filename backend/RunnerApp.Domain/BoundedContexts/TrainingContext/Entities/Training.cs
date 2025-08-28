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

    private Training(LocationType location, double distance, TimeSpan duration, DateTime date)
    {
        Id = IdValueObject.New();
        Location = location;

        if (distance <= 0)
            throw new ArgumentException("Distance must be greater than zero.");
        Distance = distance;

        if (duration <= TimeSpan.Zero)
            throw new ArgumentException("Duration must be greater than zero.");
        Duration = duration;

        Date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        CreatedAt = DateTime.UtcNow;
    }

    public static Training Factory(LocationType location, double distance, TimeSpan duration, DateTime date)
        => new Training(location, distance, duration, date);
}
