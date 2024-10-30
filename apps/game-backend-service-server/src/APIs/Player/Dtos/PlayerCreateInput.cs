namespace GameBackendService.APIs.Dtos;

public class PlayerCreateInput
{
    public List<Character>? Characters { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public int? Level { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Username { get; set; }
}
