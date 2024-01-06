using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalker.Data;
using NZwalker.Models.Domain;
using NZwalker.Models.DTO;
using NZwalker.Repositories.IRepo;

namespace NZwalker.Controller;

[Route("api/[Controller]")]
[ApiController]

public class RegionController(NZWalksDbContext dbContext, IRegionRepository regionRepository) : ControllerBase
{

    private readonly NZWalksDbContext dbContext = dbContext;
    private readonly IRegionRepository regionRepository = regionRepository;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Get data from Database - Domain Model
        var regionsDomain = await regionRepository.GetAllAsync();

        // Map Domain model to DTO
        List<RegionDto> regionsDto = [];

        foreach (var region in regionsDomain)
        {
            regionsDto.Add(
                new RegionDto
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                }
            );
        }

        // Return DTO
        return Ok(regionsDto);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {

        // Get data from Database - Domain Model
        Region? regionByIdDomain = await regionRepository.GetById(id);

        // Check if it is 
        if (regionByIdDomain == null)
        {
            return NotFound();
        }

        // Map Domain model to DTO
        RegionDto regionByIdDto = new()
        {
            Id = regionByIdDomain.Id,
            Name = regionByIdDomain.Name,
            Code = regionByIdDomain.Code,
            RegionImageUrl = regionByIdDomain.RegionImageUrl
        };

        // Return DTO
        return Ok(regionByIdDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
    {
        // Map Dto --> Domain model
        Region regionDomainModel = new()
        {
            Name = addRegionRequestDto.Name,
            Code = addRegionRequestDto.Code,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl
        };

        // Domain Model to create Region
        await dbContext.Regions.AddAsync(regionDomainModel);
        await dbContext.SaveChangesAsync();


        // Map Domain model --> DTO
        RegionDto regionDto = new()
        {
            Id = regionDomainModel.Id,
            Name = regionDomainModel.Name,
            Code = regionDomainModel.Code,
            RegionImageUrl = regionDomainModel.RegionImageUrl,
        };

        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
    {
        // check if the region is exist 
        Region? regionByIdDomain = await dbContext.Regions.FindAsync(id);
        if (regionByIdDomain == null)
        {
            return NotFound();
        }

        // update region ------------------

        // Map DTO --> Domain Model
        regionByIdDomain.Code = updateRegionRequestDto.Code;
        regionByIdDomain.Name = updateRegionRequestDto.Name;
        regionByIdDomain.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

        await dbContext.SaveChangesAsync();

        // Map Domain model --> DTO
        RegionDto regionDto = new()
        {
            Id = regionByIdDomain.Id,
            Code = regionByIdDomain.Code,
            Name = regionByIdDomain.Name,
            RegionImageUrl = regionByIdDomain.RegionImageUrl,
        };

        return Ok(regionDto);

    }

    [HttpDelete]
    [Route("{id:Guid}")]

    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Region? regionDomainModel = await dbContext.Regions.FindAsync(id);

        if (regionDomainModel == null)
        {
            return NotFound();
        }

        dbContext.Regions.Remove(regionDomainModel);
        await dbContext.SaveChangesAsync();

        // Map Domain Model --> DTO
        DeleteRegionRequestDto deleteRegionRequestDto = new()
        {
            Id = regionDomainModel.Id,
            Name = regionDomainModel.Name,
            Code = regionDomainModel.Code,
            RegionImageUrl = regionDomainModel.RegionImageUrl,
        };

        return Ok(deleteRegionRequestDto);
    }


}