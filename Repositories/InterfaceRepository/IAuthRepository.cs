
using Microsoft.AspNetCore.Identity;

namespace NZwalker.Repositories.InterfaceRepo;

public interface IAuthRepository
{
    string CreateJWTToken(IdentityUser user, List<string> roles);
} 