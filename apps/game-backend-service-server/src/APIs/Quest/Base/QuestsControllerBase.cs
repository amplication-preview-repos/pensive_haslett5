using GameBackendService.APIs;
using GameBackendService.APIs.Common;
using GameBackendService.APIs.Dtos;
using GameBackendService.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameBackendService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class QuestsControllerBase : ControllerBase
{
    protected readonly IQuestsService _service;

    public QuestsControllerBase(IQuestsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Quest
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Quest>> CreateQuest(QuestCreateInput input)
    {
        var quest = await _service.CreateQuest(input);

        return CreatedAtAction(nameof(Quest), new { id = quest.Id }, quest);
    }

    /// <summary>
    /// Delete one Quest
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteQuest([FromRoute()] QuestWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteQuest(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Quests
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Quest>>> Quests([FromQuery()] QuestFindManyArgs filter)
    {
        return Ok(await _service.Quests(filter));
    }

    /// <summary>
    /// Meta data about Quest records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> QuestsMeta([FromQuery()] QuestFindManyArgs filter)
    {
        return Ok(await _service.QuestsMeta(filter));
    }

    /// <summary>
    /// Get one Quest
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Quest>> Quest([FromRoute()] QuestWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Quest(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Quest
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateQuest(
        [FromRoute()] QuestWhereUniqueInput uniqueId,
        [FromQuery()] QuestUpdateInput questUpdateDto
    )
    {
        try
        {
            await _service.UpdateQuest(uniqueId, questUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
