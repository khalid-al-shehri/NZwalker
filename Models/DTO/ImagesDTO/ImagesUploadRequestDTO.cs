
namespace NZwalker.Models.DTO;

public class ImagesUploadRequestDTO{
    public required IFormFile File { get; set; }

    public required string FileName { get; set; }

    public string? FileDescription { get; set; }
}