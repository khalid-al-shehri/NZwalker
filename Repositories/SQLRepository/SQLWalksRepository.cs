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

    public async Task<List<Walks>?> GetAll(){
        return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
    }
    
    public async Task<Walks?> GetById(Guid id){
        return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Walks?> Update(Guid id, UpdateWalksDTO updateWalksDTO){
        Walks? existingWalk = await dbContext.Walks.FindAsync(id);

        if(existingWalk == null){
            return null;
        }

        if(updateWalksDTO.Name != null){
            existingWalk.Name = updateWalksDTO.Name;
        }
        if(updateWalksDTO.Description != null){
            existingWalk.Description = updateWalksDTO.Description;
        }
        if(updateWalksDTO.WalkImageUrl != null){
            existingWalk.WalkImageUrl = updateWalksDTO.WalkImageUrl;
        }
        if(updateWalksDTO.DifficultyId != null){
            existingWalk.DifficultyId = (Guid)updateWalksDTO.DifficultyId;
        }
        if(updateWalksDTO.RegionId != null){
            existingWalk.RegionId = (Guid)updateWalksDTO.RegionId;
        }

        await dbContext.SaveChangesAsync();

        await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);

        return existingWalk;
    }




}