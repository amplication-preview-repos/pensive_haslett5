using GameBackendService.APIs;
using GameBackendService.APIs.Common;
using GameBackendService.APIs.Dtos;
using GameBackendService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameBackendService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CharactersControllerBase : ControllerBase
{
    protected readonly ICharactersService _service;

    public CharactersControllerBase(ICharactersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Character
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Character>> CreateCharacter(CharacterCreateInput input)
    {
        var character = await _service.CreateCharacter(input);

        return CreatedAtAction(nameof(Character), new { id = character.Id }, character);
    }

    /// <summary>
    /// Delete one Character
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteCharacter(
        [FromRoute()] CharacterWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteCharacter(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Characters
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Character>>> Characters(
        [FromQuery()] CharacterFindManyArgs filter
    )
    {
        return Ok(await _service.Characters(filter));
    }

    /// <summary>
    /// Meta data about Character records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CharactersMeta(
        [FromQuery()] CharacterFindManyArgs filter
    )
    {
        return Ok(await _service.CharactersMeta(filter));
    }

    /// <summary>
    /// Get one Character
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Character>> Character(
        [FromRoute()] CharacterWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Character(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Character
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateCharacter(
        [FromRoute()] CharacterWhereUniqueInput uniqueId,
        [FromQuery()] CharacterUpdateInput characterUpdateDto
    )
    {
        try
        {
            await _service.UpdateCharacter(uniqueId, characterUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Player record for Character
    /// </summary>
    [HttpGet("{Id}/player")]
    public async Task<ActionResult<List<Player>>> GetPlayer(
        [FromRoute()] CharacterWhereUniqueInput uniqueId
    )
    {
        var player = await _service.GetPlayer(uniqueId);
        return Ok(player);
    }
}
