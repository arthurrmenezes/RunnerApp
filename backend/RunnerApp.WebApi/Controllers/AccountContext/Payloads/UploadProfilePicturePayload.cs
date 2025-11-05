namespace RunnerApp.WebApi.Controllers.AccountContext.Payloads;

public class UploadProfilePicturePayload
{
    public IFormFile File { get; init; }

    public UploadProfilePicturePayload() { }

    public UploadProfilePicturePayload(IFormFile file)
    {
        File = file; 
    }
}
