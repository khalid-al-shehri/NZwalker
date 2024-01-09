using System.ComponentModel.DataAnnotations;

namespace NZwalker.Models.DTO;

public class AddRegionRequestDto{

    [MinLength(3, ErrorMessage = "Code has to be 3 characters")]
    [MaxLength(3, ErrorMessage = "Code has to be 3 characters")]
    public required string Code { get; set; }
    
    [MaxLength(40, ErrorMessage = "Name has to be 40 characters")]
    public required string Name { get; set; }
    public string? RegionImageUrl { get; set; }
}