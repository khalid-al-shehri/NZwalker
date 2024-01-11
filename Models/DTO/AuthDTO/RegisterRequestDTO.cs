
using System.ComponentModel.DataAnnotations;

namespace NZwalker.Models.DTO;

public class RegisterRequestDTO{
    
    [DataType(DataType.EmailAddress)]
    public required string Username {get; set;}

    [DataType(DataType.Password)]
    public required string Password {get; set;}

    public required string[] Roles {get; set;}
}