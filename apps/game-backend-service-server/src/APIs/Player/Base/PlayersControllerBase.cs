using GameBackendService.APIs;
using GameBackendService.APIs.Common;
using GameBackendService.APIs.Dtos;
using GameBackendService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameBackendService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class PlayersControllerBase : ControllerBase
{
    protected readonly IPlayersService _service;

    public PlayersControllerBase(IPlayersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Player
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Player>> CreatePlayer(PlayerCreateInput input)
    {
        var player = await _service.CreatePlayer(input);

        return CreatedAtAction(nameof(Player), new { id = player.Id }, player);
    }

    /// <summary>
    /// Delete one Player
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeletePlayer([FromRoute()] PlayerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeletePlayer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Players
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Player>>> Players([FromQuery()] PlayerFindManyArgs filter)
    {
        return Ok(await _service.Players(filter));
    }

    /// <summary>
    /// Meta data about Player records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> PlayersMeta(
        [FromQuery()] PlayerFindManyArgs filter
    )
    {
        return Ok(await _service.PlayersMeta(filter));
    }

    /// <summary>
    /// Get one Player
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Player>> Player([FromRoute()] PlayerWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Player(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Player
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdatePlayer(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromQuery()] PlayerUpdateInput playerUpdateDto
    )
    {
        try
        {
            await _service.UpdatePlayer(uniqueId, playerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Characters records to Player
    /// </summary>
    [HttpPost("{Id}/characters")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectCharacters(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromQuery()] CharacterWhereUniqueInput[] charactersId
    )
    {
        try
        {
            await _service.ConnectCharacters(uniqueId, charactersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Characters records from Player
    /// </summary>
    [HttpDelete("{Id}/characters")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectCharacters(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromBody()] CharacterWhereUniqueInput[] charactersId
    )
    {
        try
        {
            await _service.DisconnectCharacters(uniqueId, charactersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Characters records for Player
    /// </summary>
    [HttpGet("{Id}/characters")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Character>>> FindCharacters(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromQuery()] CharacterFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindCharacters(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Characters records for Player
    /// </summary>
    [HttpPatch("{Id}/characters")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateCharacters(
        [FromRoute()] PlayerWhereUniqueInput uniqueId,
        [FromBody()] CharacterWhereUniqueInput[] charactersId
    )
    {
        try
        {
            await _service.UpdateCharacters(uniqueId, charactersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
