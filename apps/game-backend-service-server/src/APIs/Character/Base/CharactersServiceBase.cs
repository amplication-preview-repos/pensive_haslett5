using GameBackendService.APIs;
using GameBackendService.APIs.Common;
using GameBackendService.APIs.Dtos;
using GameBackendService.APIs.Errors;
using GameBackendService.APIs.Extensions;
using GameBackendService.Infrastructure;
using GameBackendService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBackendService.APIs;

public abstract class CharactersServiceBase : ICharactersService
{
    protected readonly GameBackendServiceDbContext _context;

    public CharactersServiceBase(GameBackendServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Character
    /// </summary>
    public async Task<Character> CreateCharacter(CharacterCreateInput createDto)
    {
        var character = new CharacterDbModel
        {
            ClassType = createDto.ClassType,
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            character.Id = createDto.Id;
        }
        if (createDto.Player != null)
        {
            character.Player = await _context
                .Players.Where(player => createDto.Player.Id == player.Id)
                .FirstOrDefaultAsync();
        }

        _context.Characters.Add(character);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CharacterDbModel>(character.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Character
    /// </summary>
    public async Task DeleteCharacter(CharacterWhereUniqueInput uniqueId)
    {
        var character = await _context.Characters.FindAsync(uniqueId.Id);
        if (character == null)
        {
            throw new NotFoundException();
        }

        _context.Characters.Remove(character);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Characters
    /// </summary>
    public async Task<List<Character>> Characters(CharacterFindManyArgs findManyArgs)
    {
        var characters = await _context
            .Characters.Include(x => x.Player)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return characters.ConvertAll(character => character.ToDto());
    }

    /// <summary>
    /// Meta data about Character records
    /// </summary>
    public async Task<MetadataDto> CharactersMeta(CharacterFindManyArgs findManyArgs)
    {
        var count = await _context.Characters.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Character
    /// </summary>
    public async Task<Character> Character(CharacterWhereUniqueInput uniqueId)
    {
        var characters = await this.Characters(
            new CharacterFindManyArgs { Where = new CharacterWhereInput { Id = uniqueId.Id } }
        );
        var character = characters.FirstOrDefault();
        if (character == null)
        {
            throw new NotFoundException();
        }

        return character;
    }

    /// <summary>
    /// Update one Character
    /// </summary>
    public async Task UpdateCharacter(
        CharacterWhereUniqueInput uniqueId,
        CharacterUpdateInput updateDto
    )
    {
        var character = updateDto.ToModel(uniqueId);

        if (updateDto.Player != null)
        {
            character.Player = await _context
                .Players.Where(player => updateDto.Player == player.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(character).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Characters.Any(e => e.Id == character.Id))
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
    /// Get a Player record for Character
    /// </summary>
    public async Task<Player> GetPlayer(CharacterWhereUniqueInput uniqueId)
    {
        var character = await _context
            .Characters.Where(character => character.Id == uniqueId.Id)
            .Include(character => character.Player)
            .FirstOrDefaultAsync();
        if (character == null)
        {
            throw new NotFoundException();
        }
        return character.Player.ToDto();
    }
}
