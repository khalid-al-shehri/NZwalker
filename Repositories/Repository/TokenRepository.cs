using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NZwalker.Repositories.InterfaceRepo;

namespace NZwalker.Repositories.Repo;

public class TokenRepository (IConfiguration configuration) : IAuthRepository
{
    private readonly IConfiguration configuration = configuration;

    public string CreateJWTToken(IdentityUser user, List<string> roles)
    {
        // Create claims
        List<Claim> claims = new(){
            new Claim(ClaimTypes.Email, user.Email)
        };

        foreach(var role in roles){
            claims.Add( new Claim(ClaimTypes.Role, role));
        }

        // provide key from appsettings.json
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

        // create credentials
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // create JWT Security token
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials
        );

        // Generate token
        string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return token;
    }
}