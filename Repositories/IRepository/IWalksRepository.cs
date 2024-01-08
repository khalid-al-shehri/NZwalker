
using NZwalker.Models.Domain;

namespace NZwalker.Repositories.IRepo;

public interface IWalksRepository{

    Task<Walks> Create(Walks walks);
    Task<List<Walks>?> GetAll();
    Task<Walks?> GetById(Guid id);

    Task<Walks?> Update(Guid id, UpdateWalksDTO updateWalksDTO);

}