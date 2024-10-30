using GameBackendService.APIs;
using GameBackendService.APIs.Common;
using GameBackendService.APIs.Dtos;
using GameBackendService.APIs.Errors;
using GameBackendService.APIs.Extensions;
using GameBackendService.Infrastructure;
using GameBackendService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBackendService.APIs;

public abstract class QuestsServiceBase : IQuestsService
{
    protected readonly GameBackendServiceDbContext _context;

    public QuestsServiceBase(GameBackendServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Quest
    /// </summary>
    public async Task<Quest> CreateQuest(QuestCreateInput createDto)
    {
        var quest = new QuestDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            QuestName = createDto.QuestName,
            Rewards = createDto.Rewards,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            quest.Id = createDto.Id;
        }

        _context.Quests.Add(quest);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<QuestDbModel>(quest.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Quest
    /// </summary>
    public async Task DeleteQuest(QuestWhereUniqueInput uniqueId)
    {
        var quest = await _context.Quests.FindAsync(uniqueId.Id);
        if (quest == null)
        {
            throw new NotFoundException();
        }

        _context.Quests.Remove(quest);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Quests
    /// </summary>
    public async Task<List<Quest>> Quests(QuestFindManyArgs findManyArgs)
    {
        var quests = await _context
            .Quests.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return quests.ConvertAll(quest => quest.ToDto());
    }

    /// <summary>
    /// Meta data about Quest records
    /// </summary>
    public async Task<MetadataDto> QuestsMeta(QuestFindManyArgs findManyArgs)
    {
        var count = await _context.Quests.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Quest
    /// </summary>
    public async Task<Quest> Quest(QuestWhereUniqueInput uniqueId)
    {
        var quests = await this.Quests(
            new QuestFindManyArgs { Where = new QuestWhereInput { Id = uniqueId.Id } }
        );
        var quest = quests.FirstOrDefault();
        if (quest == null)
        {
            throw new NotFoundException();
        }

        return quest;
    }

    /// <summary>
    /// Update one Quest
    /// </summary>
    public async Task UpdateQuest(QuestWhereUniqueInput uniqueId, QuestUpdateInput updateDto)
    {
        var quest = updateDto.ToModel(uniqueId);

        _context.Entry(quest).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Quests.Any(e => e.Id == quest.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
