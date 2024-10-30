using GameBackendService.APIs.Common;
using GameBackendService.APIs.Dtos;

namespace GameBackendService.APIs;

public interface ICharactersService
{
    /// <summary>
    /// Create one Character
    /// </summary>
    public Task<Character> CreateCharacter(CharacterCreateInput character);

    /// <summary>
    /// Delete one Character
    /// </summary>
    public Task DeleteCharacter(CharacterWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Characters
    /// </summary>
    public Task<List<Character>> Characters(CharacterFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Character records
    /// </summary>
    public Task<MetadataDto> CharactersMeta(CharacterFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Character
    /// </summary>
    public Task<Character> Character(CharacterWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Character
    /// </summary>
    public Task UpdateCharacter(CharacterWhereUniqueInput uniqueId, CharacterUpdateInput updateDto);

    /// <summary>
    /// Get a Player record for Character
    /// </summary>
    public Task<Player> GetPlayer(CharacterWhereUniqueInput uniqueId);
}
