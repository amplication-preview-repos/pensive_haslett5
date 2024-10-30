namespace GameBackendService.APIs.Dtos;

public class CharacterCreateInput
{
    public string? ClassType { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public Player? Player { get; set; }

    public DateTime UpdatedAt { get; set; }
}
