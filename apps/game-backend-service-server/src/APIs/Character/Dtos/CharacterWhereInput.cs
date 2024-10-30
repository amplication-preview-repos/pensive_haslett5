namespace GameBackendService.APIs.Dtos;

public class CharacterWhereInput
{
    public string? ClassType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Player { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
