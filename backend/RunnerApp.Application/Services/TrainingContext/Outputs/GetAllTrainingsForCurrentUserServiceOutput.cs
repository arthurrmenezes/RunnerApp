using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;

namespace RunnerApp.Application.Services.AccountContext.Outputs;

public readonly struct GetAllTrainingsForCurrentUserServiceOutput
{
    public int TotalTrainings { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public GetAllTrainingsByAccountIdServiceOutputTrainingOutput[] Trainings { get; }

    private GetAllTrainingsForCurrentUserServiceOutput(int totalTrainings, int pageNumber, int pageSize, int totalPages,
        GetAllTrainingsByAccountIdServiceOutputTrainingOutput[] trainings)
    {
        TotalTrainings = totalTrainings;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = totalPages;
        Trainings = trainings;
    }

    public static GetAllTrainingsForCurrentUserServiceOutput Factory(int totalTrainings, int pageNumber, int pageSize, int totalPages,
        GetAllTrainingsByAccountIdServiceOutputTrainingOutput[] trainings)
        => new GetAllTrainingsForCurrentUserServiceOutput(totalTrainings, pageNumber, pageSize, totalPages, trainings);
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
