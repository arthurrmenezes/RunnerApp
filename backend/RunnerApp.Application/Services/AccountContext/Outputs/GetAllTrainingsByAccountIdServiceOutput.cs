using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;

namespace RunnerApp.Application.Services.AccountContext.Outputs;

public readonly struct GetAllTrainingsByAccountIdServiceOutput
{
    public int TotalTrainings { get; }
    public GetAllTrainingsByAccountIdServiceOutputTrainingOutput[] Trainings { get; }

    private GetAllTrainingsByAccountIdServiceOutput(int totalTrainings, GetAllTrainingsByAccountIdServiceOutputTrainingOutput[] trainings)
    {
        TotalTrainings = totalTrainings;
        Trainings = trainings;
    }

    public static GetAllTrainingsByAccountIdServiceOutput Factory(int totalTrainings, GetAllTrainingsByAccountIdServiceOutputTrainingOutput[] trainings)
        => new GetAllTrainingsByAccountIdServiceOutput(totalTrainings, trainings);
}

public sealed record GetAllTrainingsByAccountIdServiceOutputTrainingOutput
{
    public string Id { get; }
    public LocationType Location { get; }
    public double Distance { get; }
    public TimeSpan Duration { get; }
    public DateTime Date { get; }
    public DateTime CreatedAt { get; }

    public GetAllTrainingsByAccountIdServiceOutputTrainingOutput(string id, LocationType location, double distance, TimeSpan duration, DateTime date, DateTime createdAt)
    {
        Id = id;
        Location = location;
        Distance = distance;
        Duration = duration;
        Date = date;
        CreatedAt = createdAt;
    }
}
