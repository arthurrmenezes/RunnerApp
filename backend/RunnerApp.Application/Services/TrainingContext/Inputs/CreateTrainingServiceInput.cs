using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;

namespace RunnerApp.Application.Services.TrainingContext.Inputs;

public readonly struct CreateTrainingServiceInput
{
    public LocationType Location { get; }
    public double Distance { get; }
    public TimeSpan Duration { get; }
    public DateTime Date { get; }

    private CreateTrainingServiceInput(LocationType location, double distance, TimeSpan duration, DateTime date)
    {
        Location = location;
        Distance = distance;
        Duration = duration;
        Date = date;
    }

    public static CreateTrainingServiceInput Factory(LocationType location, double distance, TimeSpan duration, DateTime date)
        => new CreateTrainingServiceInput(location, distance, duration, date);
}
