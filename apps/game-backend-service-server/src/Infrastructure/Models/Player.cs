using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameBackendService.Infrastructure.Models;

[Table("Players")]
public class PlayerDbModel
{
    public List<CharacterDbModel>? Characters { get; set; } = new List<CharacterDbModel>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Range(-999999999, 999999999)]
    public int? Level { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? Username { get; set; }
}
