namespace RunnerApp.Application.Services.AuthContext.Inputs;

public readonly struct ChangePasswordInput
{
    public string UserId { get; }
    public string CurrentPassword { get; }
    public string NewPassword { get; }

    private ChangePasswordInput(string userId, string currentPassword, string newPassword)
    {
        UserId = userId;
        CurrentPassword = currentPassword;
        NewPassword = newPassword;
    }

    public static ChangePasswordInput Factory(string userId, string currentPassword, string newPassword)
        => new ChangePasswordInput(userId, currentPassword, newPassword);
}
