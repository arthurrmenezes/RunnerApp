using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RunnerApp.Infrastructure.Files.Interfaces;
using RunnerApp.Infrastructure.Files.Outputs;

namespace RunnerApp.Infrastructure.Files;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    private const string profilePicturePath = "uploads";
    private const double maxFileSize = 5 * 1024 * 1024; // 5 MB

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<UploadFileServiceOutput> UploadFileServiceAsync(IFormFile file, CancellationToken cancellationToken)
    {
        var path = Path.Combine(_webHostEnvironment.WebRootPath, profilePicturePath);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(fileExtension))
            throw new InvalidOperationException("Invalid file type. Only PNG, JPG and JPEG are allowed.");

        if (file.Length <= 0)
            throw new InvalidOperationException("Error. The file is empty.");

        if (file.Length > maxFileSize)
            throw new InvalidOperationException($"File too large. Maximum size is {maxFileSize / (1024 * 1024)} MB.");

        var fileName = $"{Guid.NewGuid()}{fileExtension}";
        var filePath = Path.Combine(path, fileName);

        await using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream, cancellationToken);
        }

        return UploadFileServiceOutput.Factory(
            fileName: fileName,
            fileUrl: $"/{profilePicturePath}/{fileName}".Replace("\\", "/"),
            fileSize: file.Length);
    }

    public void DeleteFile(string filePath, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new InvalidOperationException("Error. Invalid file path.");

        var path = Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart('/').Replace("/", "\\"));

        if (File.Exists(path))
            File.Delete(path);
    }
}
