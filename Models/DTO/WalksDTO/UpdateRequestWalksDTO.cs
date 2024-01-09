
using System.ComponentModel.DataAnnotations;

namespace NZwalker.Models.Domain;

public class UpdateRequestWalksDTO{
    [MinLength(4)]
    [MaxLength(60)]
    public string? Name { get; set; }

    [MinLength(4)]
    [MaxLength(150)]
    public string? Description { get; set; }

    public string? WalkImageUrl { get; set; }

    public Guid? DifficultyId { get; set; }
    public Guid? RegionId { get; set; }

}