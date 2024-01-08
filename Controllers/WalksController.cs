
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NZwalker.Data;
using NZwalker.Models.Domain;
using NZwalker.Repositories.IRepo;

namespace NZwalker.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class WalksController(IWalksRepository walksRepository, IMapper mapper) : ControllerBase
{

    private readonly IWalksRepository walksRepository = walksRepository;
    private readonly IMapper mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Walks>? walks = await walksRepository.GetAll();

        if(walks == null){
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

        if(WalksById == null){
            return NotFound();
        }

        // Map domain Model --> DTO
        WalksDTO walkByIdDTO = mapper.Map<WalksDTO>(WalksById);

        return Ok(walkByIdDTO);

    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddWalksDTO addWalksDTO)
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
    public async Task<IActionResult> Update([FromRoute] Guid id ,[FromBody] UpdateWalksDTO updateWalksDTO){
        Walks? updateExistingWalk = await walksRepository.Update(id, updateWalksDTO);

        if(updateExistingWalk == null){
            return NotFound();
        }

        WalksDTO updatedWalkDTO = mapper.Map<WalksDTO>(updateExistingWalk);

        return Ok(updatedWalkDTO);

    }
}