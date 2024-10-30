using GameBackendService.APIs.Dtos;
using GameBackendService.Infrastructure.Models;

namespace GameBackendService.APIs.Extensions;

public static class PlayersExtensions
{
    public static Player ToDto(this PlayerDbModel model)
    {
        return new Player
        {
            Characters = model.Characters?.Select(x => x.Id).ToList(),
            CreatedAt = model.CreatedAt,
            Email = model.Email,
            Id = model.Id,
            Level = model.Level,
            UpdatedAt = model.UpdatedAt,
            Username = model.Username,
        };
    }

    public static PlayerDbModel ToModel(
        this PlayerUpdateInput updateDto,
        PlayerWhereUniqueInput uniqueId
    )
    {
        var player = new PlayerDbModel
        {
            Id = uniqueId.Id,
            Email = updateDto.Email,
            Level = updateDto.Level,
            Username = updateDto.Username
        };

        if (updateDto.CreatedAt != null)
        {
            player.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            player.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return player;
    }
}
