using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalker.CustomActionFilters;
using NZwalker.Data;
using NZwalker.Models.Domain;
using NZwalker.Models.DTO;
using NZwalker.Repositories.InterfaceRepo;

namespace NZwalker.Controllers;

[Route("api/v{version:apiVersion}/[Controller]")]
[ApiController]
[ApiVersion("1.0")]
[ApiVersion("2.0")]

public class RegionController(
    IRegionRepository regionRepository, 
    IMapper mapper, 
    ILogger<RegionController> logger
) : ControllerBase
{

    private readonly IRegionRepository regionRepository = regionRepository;
    private readonly IMapper mapper = mapper;
    private readonly ILogger<RegionController> logger = logger;

    [MapToApiVersion("1.0")]
    [HttpGet]
    public async Task<IActionResult> GetAllV1()
    {
        // Get data from Database - Domain Model
        var regionsDomain = await regionRepository.GetAllAsync();
        
        logger.LogInformation($"Finished of GetAll Regions with Data : {JsonSerializer.Serialize(regionsDomain)}");
        // Map Domain model to DTO
        List<RegionDto> regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

        // Return DTO
        return Ok(regionsDto);
    }


    [MapToApiVersion("2.0")]
    [HttpGet]
    public IActionResult GetAllV2()
    {
        List<RegionDto> regionsDto = [];
        regionsDto.Add(
            new RegionDto{
                Id = Guid.NewGuid(),
                Code = "Version 2",
                Name = "Version 2",
                RegionImageUrl = "Version 2"
            }
        );

        // Return DTO
        return Ok(regionsDto);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Reader")]
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
        RegionDto regionByIdDto = mapper.Map<RegionDto>(regionByIdDomain);

        // Return DTO
        return Ok(regionByIdDto);
    }

    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
    {
        // Map Dto --> Domain model
        Region regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

        // Domain Model to create Region
        regionDomainModel = await regionRepository.Create(regionDomainModel);

        // Map Domain model --> DTO
        RegionDto regionDto = mapper.Map<RegionDto>(regionDomainModel);

        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
    {

        // Map DTO --> Domain Model
        Region regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

        // check if the region is exist and update in Repository layer
        Region? regionByIdDomain = await regionRepository.Update(id, regionDomainModel);
        
        if (regionByIdDomain == null)
        {
            return NotFound();
        }

        // Map Domain model --> DTO
        RegionDto regionDto = mapper.Map<RegionDto>(regionByIdDomain);

        return Ok(regionDto);

    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]

    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Region? deleteExistingRegion = await regionRepository.Delete(id);

        if (deleteExistingRegion == null)
        {
            return NotFound();
        }

        // Map Domain Model --> DTO
        DeleteRegionRequestDto deleteRegionRequestDto = mapper.Map<DeleteRegionRequestDto>(deleteExistingRegion);

        return Ok(deleteRegionRequestDto);
    }

}