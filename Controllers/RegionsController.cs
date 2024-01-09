using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalker.Data;
using NZwalker.Models.Domain;
using NZwalker.Models.DTO;
using NZwalker.Repositories.IRepo;

namespace NZwalker.Controllers;

[Route("api/[Controller]")]
[ApiController]

public class RegionController(IRegionRepository regionRepository, IMapper mapper) : ControllerBase
{

    private readonly IRegionRepository regionRepository = regionRepository;
    private readonly IMapper mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Get data from Database - Domain Model
        var regionsDomain = await regionRepository.GetAllAsync();

        // Map Domain model to DTO
        List<RegionDto> regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);

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
        RegionDto regionByIdDto = mapper.Map<RegionDto>(regionByIdDomain);

        // Return DTO
        return Ok(regionByIdDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
    {

        if(!ModelState.IsValid){
            Console.WriteLine(ModelState);
            return BadRequest(ModelState);
        }

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