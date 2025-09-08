using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;

namespace RunnerApp.Application.Services.TrainingContext.Outputs;

public readonly struct GetTrainingByIdServiceOutput
{
    public string Id { get; }
    public LocationType Location { get; }
    public double Distance { get; }
    public TimeSpan Duration { get; }
    public DateTime Date { get; }
    public DateTime CreatedAt { get; }
    public GetTrainingByIdServiceOutputAccountOutput Account { get; }

    private GetTrainingByIdServiceOutput(string id, LocationType location, double distance, TimeSpan duration, DateTime date, DateTime createdAt, GetTrainingByIdServiceOutputAccountOutput account)
    {
        Id = id;
        Location = location;
        Distance = distance;
        Duration = duration;
        Date = date;
        CreatedAt = createdAt;
        Account = account;
    }

    public static GetTrainingByIdServiceOutput Factory(string id, LocationType location, double distance, TimeSpan duration, DateTime date, DateTime createdAt, GetTrainingByIdServiceOutputAccountOutput account)
        => new GetTrainingByIdServiceOutput(id, location, distance, duration, date, createdAt, account);
}

public sealed record GetTrainingByIdServiceOutputAccountOutput
{
    public string Id { get; }
    public string FirstName { get; }
    public string Surname { get; }
    public string Email { get; }
    public DateTime CreatedAt { get; }

    public GetTrainingByIdServiceOutputAccountOutput(string id, string firstName, string surname, string email, DateTime createdAt)
    {
        Id = id;
        FirstName = firstName;
        Surname = surname;
        Email = email;
        CreatedAt = createdAt;
    }
}
