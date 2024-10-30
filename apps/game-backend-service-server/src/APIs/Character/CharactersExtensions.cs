using GameBackendService.APIs.Dtos;
using GameBackendService.Infrastructure.Models;

namespace GameBackendService.APIs.Extensions;

public static class CharactersExtensions
{
    public static Character ToDto(this CharacterDbModel model)
    {
        return new Character
        {
            ClassType = model.ClassType,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Name = model.Name,
            Player = model.PlayerId,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CharacterDbModel ToModel(
        this CharacterUpdateInput updateDto,
        CharacterWhereUniqueInput uniqueId
    )
    {
        var character = new CharacterDbModel
        {
            Id = uniqueId.Id,
            ClassType = updateDto.ClassType,
            Name = updateDto.Name
        };

        if (updateDto.CreatedAt != null)
        {
            character.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Player != null)
        {
            character.PlayerId = updateDto.Player;
        }
        if (updateDto.UpdatedAt != null)
        {
            character.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return character;
    }
}
