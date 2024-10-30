using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBackendService.Infrastructure.Models;

[Table("Characters")]
public class CharacterDbModel
{
    [StringLength(1000)]
    public string? ClassType { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public string? PlayerId { get; set; }

    [ForeignKey(nameof(PlayerId))]
    public PlayerDbModel? Player { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
