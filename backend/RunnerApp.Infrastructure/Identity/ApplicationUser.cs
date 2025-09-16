using Microsoft.AspNetCore.Identity;
using RunnerApp.Domain.BoundedContexts.AccountContext.Entities;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public IdValueObject AccountId { get; set; }
    public virtual Account Account { get; set; }
}
