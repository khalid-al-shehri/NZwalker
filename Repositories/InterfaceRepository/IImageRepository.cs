using NZwalker.Models.Domain;

namespace NZwalker.Repositories.InterfaceRepo;

public interface IImageRepository
{
    Task<Image> Upload(Image image);
} 