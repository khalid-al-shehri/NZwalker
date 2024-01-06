
using NZwalker.Models.Domain;

namespace NZwalker.Repositories.IRepo;

public interface IRegionRepository{
    Task<List<Region>> GetAllAsync();
    Task<Region?> GetById(Guid id);
}