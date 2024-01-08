
using NZwalker.Models.Domain;

namespace NZwalker.Repositories.IRepo;

public interface IWalksRepository{

    Task<Walks> Create(Walks walks);
    Task<List<Walks>?> GetAll();

}