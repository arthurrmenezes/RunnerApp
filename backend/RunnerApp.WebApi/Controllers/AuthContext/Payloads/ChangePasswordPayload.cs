namespace RunnerApp.WebApi.Controllers.AuthContext.Payloads;

public class ChangePasswordPayload
{
    public string CurrentPassword { get; init; }
    public string NewPassword { get; init; }
    public string ConfirmNewPassword { get; init; }

    public ChangePasswordPayload(string currentPassword, string newPassword, string confirmNewPassword)
    {
        CurrentPassword = currentPassword;
        NewPassword = newPassword;
        ConfirmNewPassword = confirmNewPassword;
    }
}
