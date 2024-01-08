using Microsoft.EntityFrameworkCore;
using NZwalker.Data;
using NZwalker.Models.Domain;
using NZwalker.Repositories.IRepo;

namespace NZwalker.Repositories.SQLRepo;

public class SQLWalksRepository(NZWalksDbContext dbContext) : IWalksRepository
{
    private readonly NZWalksDbContext dbContext = dbContext;

    public async Task<Walks> Create(Walks walks){
        await dbContext.Walks.AddAsync(walks);
        await dbContext.SaveChangesAsync();

        return walks;
    }
}