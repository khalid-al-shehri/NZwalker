using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalker.Models.Domain;
using NZwalker.Models.DTO;
using NZwalker.Repositories.InterfaceRepo;

namespace NZwalker.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class ImagesController(IImageRepository imageRepository) : ControllerBase{

    private readonly IImageRepository imageRepository = imageRepository;

    [HttpPost]
    [Route("Upload")]
    public async Task<IActionResult> Upload([FromForm] ImagesUploadRequestDTO request){
        ValidateFileUpload(request);

        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        Image imageDomainModel = new(){
            File = request.File,
            FileName = request.FileName,
            FileDescription = request.FileDescription,
            FileExtension = Path.GetExtension(request.File.FileName),
            FileSizeInBytes = request.File.Length
        };

        await imageRepository.Upload(imageDomainModel);

        return Ok(imageDomainModel);

    }

    private void ValidateFileUpload(ImagesUploadRequestDTO request){
        string[] allowedExtensions = new string[] {".jpg" , ".png", ".jpeg"};

        if(!allowedExtensions.Contains(Path.GetExtension(request.File.FileName))){
            ModelState.AddModelError("file", "Unsupported file extension");
        }

        if(request.File.Length > 10485760){
            ModelState.AddModelError("file", "File size more than 10 MB");
        }

    }


}