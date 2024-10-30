using GameBackendService.APIs.Common;
using GameBackendService.APIs.Dtos;

namespace GameBackendService.APIs;

public interface IPlayersService
{
    /// <summary>
    /// Create one Player
    /// </summary>
    public Task<Player> CreatePlayer(PlayerCreateInput player);

    /// <summary>
    /// Delete one Player
    /// </summary>
    public Task DeletePlayer(PlayerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Players
    /// </summary>
    public Task<List<Player>> Players(PlayerFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Player records
    /// </summary>
    public Task<MetadataDto> PlayersMeta(PlayerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Player
    /// </summary>
    public Task<Player> Player(PlayerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Player
    /// </summary>
    public Task UpdatePlayer(PlayerWhereUniqueInput uniqueId, PlayerUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Characters records to Player
    /// </summary>
    public Task ConnectCharacters(
        PlayerWhereUniqueInput uniqueId,
        CharacterWhereUniqueInput[] charactersId
    );

    /// <summary>
    /// Disconnect multiple Characters records from Player
    /// </summary>
    public Task DisconnectCharacters(
        PlayerWhereUniqueInput uniqueId,
        CharacterWhereUniqueInput[] charactersId
    );

    /// <summary>
    /// Find multiple Characters records for Player
    /// </summary>
    public Task<List<Character>> FindCharacters(
        PlayerWhereUniqueInput uniqueId,
        CharacterFindManyArgs CharacterFindManyArgs
    );

    /// <summary>
    /// Update multiple Characters records for Player
    /// </summary>
    public Task UpdateCharacters(
        PlayerWhereUniqueInput uniqueId,
        CharacterWhereUniqueInput[] charactersId
    );
}
