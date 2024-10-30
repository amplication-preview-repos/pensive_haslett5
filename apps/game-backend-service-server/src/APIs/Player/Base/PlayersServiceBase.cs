using GameBackendService.APIs;
using GameBackendService.APIs.Common;
using GameBackendService.APIs.Dtos;
using GameBackendService.APIs.Errors;
using GameBackendService.APIs.Extensions;
using GameBackendService.Infrastructure;
using GameBackendService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBackendService.APIs;

public abstract class PlayersServiceBase : IPlayersService
{
    protected readonly GameBackendServiceDbContext _context;

    public PlayersServiceBase(GameBackendServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Player
    /// </summary>
    public async Task<Player> CreatePlayer(PlayerCreateInput createDto)
    {
        var player = new PlayerDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            Level = createDto.Level,
            UpdatedAt = createDto.UpdatedAt,
            Username = createDto.Username
        };

        if (createDto.Id != null)
        {
            player.Id = createDto.Id;
        }
        if (createDto.Characters != null)
        {
            player.Characters = await _context
                .Characters.Where(character =>
                    createDto.Characters.Select(t => t.Id).Contains(character.Id)
                )
                .ToListAsync();
        }

        _context.Players.Add(player);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<PlayerDbModel>(player.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Player
    /// </summary>
    public async Task DeletePlayer(PlayerWhereUniqueInput uniqueId)
    {
        var player = await _context.Players.FindAsync(uniqueId.Id);
        if (player == null)
        {
            throw new NotFoundException();
        }

        _context.Players.Remove(player);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Players
    /// </summary>
    public async Task<List<Player>> Players(PlayerFindManyArgs findManyArgs)
    {
        var players = await _context
            .Players.Include(x => x.Characters)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return players.ConvertAll(player => player.ToDto());
    }

    /// <summary>
    /// Meta data about Player records
    /// </summary>
    public async Task<MetadataDto> PlayersMeta(PlayerFindManyArgs findManyArgs)
    {
        var count = await _context.Players.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Player
    /// </summary>
    public async Task<Player> Player(PlayerWhereUniqueInput uniqueId)
    {
        var players = await this.Players(
            new PlayerFindManyArgs { Where = new PlayerWhereInput { Id = uniqueId.Id } }
        );
        var player = players.FirstOrDefault();
        if (player == null)
        {
            throw new NotFoundException();
        }

        return player;
    }

    /// <summary>
    /// Update one Player
    /// </summary>
    public async Task UpdatePlayer(PlayerWhereUniqueInput uniqueId, PlayerUpdateInput updateDto)
    {
        var player = updateDto.ToModel(uniqueId);

        if (updateDto.Characters != null)
        {
            player.Characters = await _context
                .Characters.Where(character =>
                    updateDto.Characters.Select(t => t).Contains(character.Id)
                )
                .ToListAsync();
        }

        _context.Entry(player).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Players.Any(e => e.Id == player.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Connect multiple Characters records to Player
    /// </summary>
    public async Task ConnectCharacters(
        PlayerWhereUniqueInput uniqueId,
        CharacterWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Players.Include(x => x.Characters)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Characters.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Characters);

        foreach (var child in childrenToConnect)
        {
            parent.Characters.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Characters records from Player
    /// </summary>
    public async Task DisconnectCharacters(
        PlayerWhereUniqueInput uniqueId,
        CharacterWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Players.Include(x => x.Characters)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Characters.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Characters?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Characters records for Player
    /// </summary>
    public async Task<List<Character>> FindCharacters(
        PlayerWhereUniqueInput uniqueId,
        CharacterFindManyArgs playerFindManyArgs
    )
    {
        var characters = await _context
            .Characters.Where(m => m.PlayerId == uniqueId.Id)
            .ApplyWhere(playerFindManyArgs.Where)
            .ApplySkip(playerFindManyArgs.Skip)
            .ApplyTake(playerFindManyArgs.Take)
            .ApplyOrderBy(playerFindManyArgs.SortBy)
            .ToListAsync();

        return characters.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Characters records for Player
    /// </summary>
    public async Task UpdateCharacters(
        PlayerWhereUniqueInput uniqueId,
        CharacterWhereUniqueInput[] childrenIds
    )
    {
        var player = await _context
            .Players.Include(t => t.Characters)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (player == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Characters.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        player.Characters = children;
        await _context.SaveChangesAsync();
    }
}
