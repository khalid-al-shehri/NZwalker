
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NZwalker.Models.Domain;

namespace NZwalker.Data;

public class NZWalksAuthDbContext : IdentityDbContext
{
    public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> dbContextOptions) : base(dbContextOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        string readerRoleId = "48f60239-8837-4a01-a8ab-77eb3dca4433";
        string writerRoleId = "49613479-2881-45eb-b458-a8e384c16ed9";
        List<IdentityRole> roles = new()
        {
            new(){
                Id = readerRoleId,
                ConcurrencyStamp = readerRoleId,
                Name = "Reader",
                NormalizedName = "Reader".ToUpper(),
            },
            new(){
                Id = writerRoleId,
                ConcurrencyStamp = writerRoleId,
                Name = "Writer",
                NormalizedName = "Writer".ToUpper(),
            },
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }
}