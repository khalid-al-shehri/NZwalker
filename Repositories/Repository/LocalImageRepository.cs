using NZwalker.Data;
using NZwalker.Models.Domain;
using NZwalker.Repositories.InterfaceRepo;

namespace NZwalker.Repositories.Repo;

public class LocalImageRepository(NZWalksDbContext dbContext ,IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor) : IImageRepository
{

    private readonly NZWalksDbContext dbContext = dbContext;
    private readonly IWebHostEnvironment webHostEnvironment = webHostEnvironment;
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;

    public async Task<Image> Upload(Image image){

        string localFilePath = Path.Combine(
            webHostEnvironment.ContentRootPath, 
            "Images", 
            $"{image.FileName}{image.FileExtension}"
        );

        // Upload Image to local path
        using var stream = new FileStream(localFilePath, FileMode.Create);
        await image.File.CopyToAsync(stream);

        // Create path for the file
        string protocol = httpContextAccessor.HttpContext.Request.Scheme;
        HostString host = httpContextAccessor.HttpContext.Request.Host;
        string pathBase = httpContextAccessor.HttpContext.Request.PathBase;

        string urlFilePath = $"{protocol}://{host}{pathBase}/Images/{image.FileName}{image.FileExtension}";

        image.FilePath = urlFilePath;

        // Add Image to database
        await dbContext.Image.AddAsync(image);
        await dbContext.SaveChangesAsync();

        return image;
    }

}