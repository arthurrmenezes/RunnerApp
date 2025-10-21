using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;

namespace RunnerApp.Application.Services.TrainingContext.Outputs;

public readonly struct UpdateTrainingByIdServiceOutput
{
    public string Id { get; }
    public LocationType Location { get; }
    public double Distance { get; }
    public TimeSpan Duration { get; }
    public DateTime Date { get; }
    public DateTime CreatedAt { get; }
    public UpdateTrainingByIdServiceOutputAccountOutput Account { get; }

    private UpdateTrainingByIdServiceOutput(string id, LocationType location, double distance, TimeSpan duration, DateTime date, DateTime createdAt, UpdateTrainingByIdServiceOutputAccountOutput account)
    {
        Id = id;
        Location = location;
        Distance = distance;
        Duration = duration;
        Date = date;
        CreatedAt = createdAt;
        Account = account;
    }

    public static UpdateTrainingByIdServiceOutput Factory(string id, LocationType location, double distance, TimeSpan duration, DateTime date, DateTime createdAt, UpdateTrainingByIdServiceOutputAccountOutput account)
        => new UpdateTrainingByIdServiceOutput(id, location, distance, duration, date, createdAt, account);
}

public sealed record UpdateTrainingByIdServiceOutputAccountOutput
{
    public string Id { get; }
    public string FirstName { get; }
    public string Surname { get; }
    public string Email { get; }
    public DateTime CreatedAt { get; }

    public UpdateTrainingByIdServiceOutputAccountOutput(string id, string firstName, string surname, string email, DateTime createdAt)
    {
        Id = id;
        FirstName = firstName;
        Surname = surname;
        Email = email;
        CreatedAt = createdAt;
    }
}
