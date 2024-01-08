
namespace NZwalker.Models.Domain;

public class UpdateWalksDTO{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? WalkImageUrl { get; set; }

    public Guid? DifficultyId { get; set; }
    public Guid? RegionId { get; set; }

}