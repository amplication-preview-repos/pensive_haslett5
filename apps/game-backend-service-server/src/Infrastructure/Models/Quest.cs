using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBackendService.Infrastructure.Models;

[Table("Quests")]
public class QuestDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? QuestName { get; set; }

    public string? Rewards { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
