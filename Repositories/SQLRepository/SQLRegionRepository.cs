using Microsoft.EntityFrameworkCore;
using NZwalker.Data;
using NZwalker.Models.Domain;
using NZwalker.Repositories.IRepo;

namespace NZwalker.Repositories.SQLRepo;

public class SQLRegionRepository(NZWalksDbContext dbContext) : IRegionRepository
{
    private readonly NZWalksDbContext dbContext = dbContext;
    public async Task<List<Region>> GetAllAsync()
    {
        return await dbContext.Regions.ToListAsync();
    }

    public async Task<Region?> GetById(Guid id){
        return await dbContext.Regions.FindAsync(id);
    }
}