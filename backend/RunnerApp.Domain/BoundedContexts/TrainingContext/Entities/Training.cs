using RunnerApp.Domain.BoundedContexts.AccountContext.Entities;
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

    public IdValueObject AccountId { get; private set; }
    public virtual Account Account { get; private set; }

    private const double MaxDistanceKm = 300;
    private static readonly TimeSpan MinDuration = TimeSpan.FromSeconds(5);
    private static readonly TimeSpan MaxDuration = TimeSpan.FromHours(24);

    private Training() { }

    private Training(LocationType location, double distance, TimeSpan duration, DateTime date, IdValueObject accountId)
    {
        Id = IdValueObject.New();
        ValidateDomain(location, distance, duration, date);

        Location = location;
        Distance = distance;
        Duration = duration;
        Date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        AccountId = accountId;
        CreatedAt = DateTime.UtcNow;
    }

    public static Training Factory(LocationType location, double distance, TimeSpan duration, DateTime date, IdValueObject accountId)
        => new Training(location, distance, duration, date, accountId);

    public void UpdateTrainingDetails(LocationType? location, double? distance, TimeSpan? duration, DateTime? date)
    {
        ValidateDomain(location, distance, duration, date);

        if (location.HasValue)
            Location = location.Value;

        if (distance.HasValue)
            Distance = distance.Value;     
        
        if (duration.HasValue)
            Duration = duration.Value;

        if (date.HasValue)
            Date = DateTime.SpecifyKind(date.Value, DateTimeKind.Utc);
    }
    
    private void ValidateDomain(LocationType? location, double? distance, TimeSpan? duration, DateTime? date)
    {
        if (location.HasValue)
            if (!Enum.IsDefined(typeof(LocationType), location))
                throw new ArgumentException("Invalid location type.", nameof(location));

        if (distance.HasValue)
            if (distance <= 0)
                throw new ArgumentException("Distance must be greater than zero.", nameof(distance));
            else if (distance > MaxDistanceKm)
                throw new ArgumentException("Distance must be less than or equal to " + MaxDistanceKm + " km.", nameof(distance));

        if (duration.HasValue)
            if (duration < MinDuration)
                throw new ArgumentException("Duration must be at least " + MinDuration.TotalSeconds + " seconds.", nameof(duration));
            if (duration > MaxDuration)
                throw new ArgumentException("Duration must be less than or equal to " + MaxDuration.TotalHours + " hours.", nameof(duration));

        if (date.HasValue)
        {
            var dateUtc = DateTime.SpecifyKind(date.Value, DateTimeKind.Utc);
            if (dateUtc > DateTime.UtcNow.AddMinutes(5))
                throw new ArgumentException("Training date cannot be in the future.", nameof(date));
        }
    }
}
