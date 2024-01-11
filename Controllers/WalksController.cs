
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NZwalker.CustomActionFilters;
using NZwalker.Data;
using NZwalker.Models.Domain;
using NZwalker.Repositories.InterfaceRepo;

namespace NZwalker.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class WalksController(IWalksRepository walksRepository, IMapper mapper) : ControllerBase
{

    private readonly IWalksRepository walksRepository = walksRepository;
    private readonly IMapper mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? param, 
        [FromQuery] string? value,
        [FromQuery] string? sortBy, 
        [FromQuery] bool? isAscending,
        [FromQuery] int? skip,
        [FromQuery] int? offset
    )
    {
        List<Walks>? walks = await walksRepository.GetAll(
            param,
            value,
            sortBy,
            skip,
            offset,
            isAscending ?? true
        );

        if (walks == null)
        {
            return NotFound();
        }

        // Map domain Model --> DTO
        List<WalksDTO> walksDTO = mapper.Map<List<WalksDTO>>(walks);

        return Ok(walksDTO);

    }

    [Route("{id:Guid}")]
    [HttpGet]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        Walks? WalksById = await walksRepository.GetById(id);

        if (WalksById == null)
        {
            return NotFound();
        }

        // Map domain Model --> DTO
        WalksDTO walkByIdDTO = mapper.Map<WalksDTO>(WalksById);

        return Ok(walkByIdDTO);

    }


    [HttpPost]
    [ValidateModel]
    public async Task<IActionResult> Create([FromBody] AddRequestWalksDTO addWalksDTO)
    {
        // map Domain Model --> DTO
        Walks walks = mapper.Map<Walks>(addWalksDTO);

        // Create record in database using Walks Repository
        Walks createWalk = await walksRepository.Create(walks);

        // map DTO --> Domain Model
        WalksDTO walksDTO = mapper.Map<WalksDTO>(createWalk);

        return Ok(walksDTO);

    }

    [Route("{id:Guid}")]
    [HttpPut]
    [ValidateModel]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRequestWalksDTO updateRequestWalksDTO)
    {
        Walks? updateExistingWalk = await walksRepository.Update(id, updateRequestWalksDTO);

        if (updateExistingWalk == null)
        {
            return NotFound();
        }

        WalksDTO updatedWalkDTO = mapper.Map<WalksDTO>(updateExistingWalk);

        return Ok(updatedWalkDTO);

    }

    [Route("{id:Guid}")]
    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        Walks? deleteExistingWalk = await walksRepository.Delete(id);

        if (deleteExistingWalk == null)
        {
            return NotFound();
        }

        DeleteResponseWalksDTO response = mapper.Map<DeleteResponseWalksDTO>(deleteExistingWalk);

        return Ok(response);

    }
}