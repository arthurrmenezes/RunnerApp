using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;

namespace RunnerApp.Application.Services.TrainingContext.Inputs;

public readonly struct CreateTrainingServiceInput
{
    public LocationType Location { get; init; }
    public double Distance { get; init; }
    public TimeSpan Duration { get; init; }
    public DateTime Date { get; init; }

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
