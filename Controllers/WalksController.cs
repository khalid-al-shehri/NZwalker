
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

}