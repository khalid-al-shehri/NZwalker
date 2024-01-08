
namespace NZwalker.Models.Domain;

public class WalksDTO{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? WalkImageUrl { get; set; }

    public Guid DifficultyId { get; set; }
    public Guid RegionId { get; set; }

    public required Difficulty Difficulty { get; set; }
    public required Region Region { get; set; }

}