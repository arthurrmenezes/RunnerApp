using RunnerApp.Domain.BoundedContexts.TrainingContext.ENUMs;

namespace RunnerApp.Application.Services.TrainingContext.Outputs;

public readonly struct CreateTrainingServiceOutput
{
    public string Id { get; }
    public LocationType Location { get; }
    public double Distance { get; }
    public TimeSpan Duration { get; }
    public DateTime Date { get; }
    public DateTime CreatedAt { get; }
    public CreateTrainingServiceOutputAccountOutput Account { get; }

    private CreateTrainingServiceOutput(string id, LocationType location, double distance, TimeSpan duration, DateTime date, DateTime createdAt, CreateTrainingServiceOutputAccountOutput account)
    {
        Id = id;
        Location = location;
        Distance = distance;
        Duration = duration;
        Date = date;
        CreatedAt = createdAt;
        Account = account;
    }

    public static CreateTrainingServiceOutput Factory(string id, LocationType location, double distance, TimeSpan duration, DateTime date, DateTime createdAt, CreateTrainingServiceOutputAccountOutput account)
        => new CreateTrainingServiceOutput(id, location, distance, duration, date, createdAt, account);
}

public sealed record CreateTrainingServiceOutputAccountOutput
{
    public string Id { get; }
    public string FirstName { get; }
    public string Surname { get; }
    public string Email { get; }
    public DateTime CreatedAt { get; }

    public CreateTrainingServiceOutputAccountOutput(string id, string firstName, string surname, string email, DateTime createdAt)
    {
        Id = id;
        FirstName = firstName;
        Surname = surname;
        Email = email;
        CreatedAt = createdAt;
    }
}
