using GameBackendService.APIs.Dtos;
using GameBackendService.Infrastructure.Models;

namespace GameBackendService.APIs.Extensions;

public static class QuestsExtensions
{
    public static Quest ToDto(this QuestDbModel model)
    {
        return new Quest
        {
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Id = model.Id,
            QuestName = model.QuestName,
            Rewards = model.Rewards,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static QuestDbModel ToModel(
        this QuestUpdateInput updateDto,
        QuestWhereUniqueInput uniqueId
    )
    {
        var quest = new QuestDbModel
        {
            Id = uniqueId.Id,
            Description = updateDto.Description,
            QuestName = updateDto.QuestName,
            Rewards = updateDto.Rewards
        };

        if (updateDto.CreatedAt != null)
        {
            quest.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            quest.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return quest;
    }
}
