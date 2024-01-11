using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZwalker.Models.DTO;
using NZwalker.Repositories.InterfaceRepo;

namespace NZwalker.Controllers;

[Route("Api/[Controller]")]
[ApiController]
public class AuthController(UserManager<IdentityUser> userManager, IAuthRepository authRepository) : ControllerBase{
    public UserManager<IdentityUser> userManager = userManager;
    public IAuthRepository authRepository = authRepository;

    [Route("Register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO requestDTO){

        var identityUser = new IdentityUser{
            UserName = requestDTO.Username,
            Email = requestDTO.Username,
        };

        IdentityResult identityResult = await userManager.CreateAsync(identityUser, requestDTO.Password);

        if(identityResult.Succeeded){
            if(requestDTO.Roles != null){
                identityResult = await userManager.AddToRolesAsync(identityUser, requestDTO.Roles);

                if(identityResult.Succeeded){
                    return Ok("User successfully registered, please login");
                }
            }
            
        }

        return BadRequest("Something went wrong");

    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request){
        
        IdentityUser? user = await userManager.FindByEmailAsync(request.Username);

        // Check if user exist or not
        if(user == null){
            return BadRequest("Username or Password not correct");
        }

        bool validPassword = await userManager.CheckPasswordAsync(user, request.Password);

        // Check if password correct
        if(validPassword == false){
            return BadRequest("Username or Password not correct");
        }

        List<string>? roles = (List<string>?)await userManager.GetRolesAsync(user);

        if(roles == null){
            return BadRequest("Username or Password not correct");            
        }

        string token = authRepository.CreateJWTToken(user, roles);

        LoginResponsesDTO response = new(){
            Token = token
        };

        return Ok(response);


    }


}