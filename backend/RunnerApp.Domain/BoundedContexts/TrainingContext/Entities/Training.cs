using System.ComponentModel.DataAnnotations;
using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;

public class Training
{
    [Key]
    public IdValueObject Id { get; set; }
    [Required]
    public LocationType Location { get; set; }
    [Required]
    public double Distance { get; set; }
    [Required]
    public TimeSpan Duration { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }

    private Training(LocationType location, double distance, TimeSpan duration, DateTime date)
    {
        Id = IdValueObject.New();
        Location = location;
        Distance = distance;
        Duration = duration;
        Date = date;
        CreatedAt = DateTime.UtcNow;
    }

    public static Training Factory(LocationType location, double distance, TimeSpan duration, DateTime date)
        => new Training(location, distance, duration, date);
}
