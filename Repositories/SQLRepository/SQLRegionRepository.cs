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

    public async Task<Region> Create(Region region){
        await dbContext.Regions.AddAsync(region);
        await dbContext.SaveChangesAsync();

        return region;
    }

    public async Task<Region?> Update(Guid id, Region region){

        Region? existingRegion = await dbContext.Regions.FindAsync(id);

        if(existingRegion == null){
            return null;
        }

        existingRegion.Code = region.Code;
        existingRegion.Name = region.Name;
        existingRegion.RegionImageUrl = region.RegionImageUrl;

        await dbContext.SaveChangesAsync();

        return existingRegion;
    }

    public async Task<Region?> Delete(Guid id){
        Region? existingRegion = await dbContext.Regions.FindAsync(id);

        if(existingRegion == null){
            return null;
        }

        dbContext.Regions.Remove(existingRegion);
        await dbContext.SaveChangesAsync();

        return existingRegion;
    }
}