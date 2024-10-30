using GameBackendService.Core.Enums;

namespace GameBackendService.APIs.Dtos;

public class Item
{
    public DateTime CreatedAt { get; set; }

    public string Id { get; set; }

    public string? ItemName { get; set; }

    public RarityEnum? Rarity { get; set; }

    public DateTime UpdatedAt { get; set; }
}
