
using NZwalker.Models.Domain;

namespace NZwalker.Repositories.IRepo;

public interface IWalksRepository{

    Task<Walks> Create(Walks walks);
    Task<List<Walks>?> GetAll(
        string? param, 
        string? value, 
        string? sortBy, 
        int? skip,
        int? offset,
        bool isAscending
    );
    Task<Walks?> GetById(Guid id);

    Task<Walks?> Update(Guid id, UpdateRequestWalksDTO updateRequestWalksDTO);
    Task<Walks?> Delete(Guid id);

}