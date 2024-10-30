namespace GameBackendService.APIs.Dtos;

public class Quest
{
    public DateTime CreatedAt { get; set; }

    public string? Description { get; set; }

    public string Id { get; set; }

    public string? QuestName { get; set; }

    public string? Rewards { get; set; }

    public DateTime UpdatedAt { get; set; }
}
