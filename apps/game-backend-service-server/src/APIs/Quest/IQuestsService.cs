using GameBackendService.APIs.Common;
using GameBackendService.APIs.Dtos;

namespace GameBackendService.APIs;

public interface IQuestsService
{
    /// <summary>
    /// Create one Quest
    /// </summary>
    public Task<Quest> CreateQuest(QuestCreateInput quest);

    /// <summary>
    /// Delete one Quest
    /// </summary>
    public Task DeleteQuest(QuestWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Quests
    /// </summary>
    public Task<List<Quest>> Quests(QuestFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Quest records
    /// </summary>
    public Task<MetadataDto> QuestsMeta(QuestFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Quest
    /// </summary>
    public Task<Quest> Quest(QuestWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Quest
    /// </summary>
    public Task UpdateQuest(QuestWhereUniqueInput uniqueId, QuestUpdateInput updateDto);
}
