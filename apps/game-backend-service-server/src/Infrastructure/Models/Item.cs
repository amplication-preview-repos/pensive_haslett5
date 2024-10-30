using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameBackendService.Core.Enums;

namespace GameBackendService.Infrastructure.Models;

[Table("Items")]
public class ItemDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? ItemName { get; set; }

    public RarityEnum? Rarity { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
