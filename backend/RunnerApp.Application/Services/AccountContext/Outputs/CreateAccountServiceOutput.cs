using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;

namespace RunnerApp.Application.Services.AccountContext.Outputs;

public readonly struct CreateAccountServiceOutput
{
    public string Id { get; }
    public string FirstName { get; }
    public string Surname { get; }
    public string Email { get; }
    public DateTime CreatedAt { get; }
    public ICollection<Training> Trainings { get; }

    private CreateAccountServiceOutput(string id, string firstName, string surname, string email, DateTime createdAt, ICollection<Training> trainings)
    {
        Id = id;
        FirstName = firstName;
        Surname = surname;
        Email = email;
        CreatedAt = createdAt;
        Trainings = trainings;
    }

    public static CreateAccountServiceOutput Factory(string id, string firstName, string surname, string email, DateTime createdAt, ICollection<Training> trainings)
        => new CreateAccountServiceOutput(id, firstName, surname, email, createdAt, trainings);
}
