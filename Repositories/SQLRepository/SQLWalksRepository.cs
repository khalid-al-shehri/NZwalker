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
        await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == walks.Id);

        return walks;
    }

    public async Task<List<Walks>?> GetAll(){
        return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
    }
    
    public async Task<Walks?> GetById(Guid id){
        return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Walks?> Update(Guid id, UpdateRequestWalksDTO updateRequestWalksDTO){
        Walks? existingWalk = await dbContext.Walks.FindAsync(id);

        if(existingWalk == null){
            return null;
        }

        if(updateRequestWalksDTO.Name != null){
            existingWalk.Name = updateRequestWalksDTO.Name;
        }
        if(updateRequestWalksDTO.Description != null){
            existingWalk.Description = updateRequestWalksDTO.Description;
        }
        if(updateRequestWalksDTO.WalkImageUrl != null){
            existingWalk.WalkImageUrl = updateRequestWalksDTO.WalkImageUrl;
        }
        if(updateRequestWalksDTO.DifficultyId != null){
            existingWalk.DifficultyId = (Guid)updateRequestWalksDTO.DifficultyId;
        }
        if(updateRequestWalksDTO.RegionId != null){
            existingWalk.RegionId = (Guid)updateRequestWalksDTO.RegionId;
        }

        await dbContext.SaveChangesAsync();

        await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);

        return existingWalk;
    }


    public async Task<Walks?> Delete(Guid id){
        
        Walks? deleteWalk = dbContext.Walks.Find(id);

        if(deleteWalk == null){
            return null;
        }

        dbContext.Walks.Remove(deleteWalk);
        await dbContext.SaveChangesAsync();

        return deleteWalk;
    } 

}