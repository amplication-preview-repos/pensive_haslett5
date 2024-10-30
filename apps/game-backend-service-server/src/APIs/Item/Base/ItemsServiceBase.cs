using GameBackendService.APIs;
using GameBackendService.APIs.Common;
using GameBackendService.APIs.Dtos;
using GameBackendService.APIs.Errors;
using GameBackendService.APIs.Extensions;
using GameBackendService.Infrastructure;
using GameBackendService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GameBackendService.APIs;

public abstract class ItemsServiceBase : IItemsService
{
    protected readonly GameBackendServiceDbContext _context;

    public ItemsServiceBase(GameBackendServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Item
    /// </summary>
    public async Task<Item> CreateItem(ItemCreateInput createDto)
    {
        var item = new ItemDbModel
        {
            CreatedAt = createDto.CreatedAt,
            ItemName = createDto.ItemName,
            Rarity = createDto.Rarity,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            item.Id = createDto.Id;
        }

        _context.Items.Add(item);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ItemDbModel>(item.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Item
    /// </summary>
    public async Task DeleteItem(ItemWhereUniqueInput uniqueId)
    {
        var item = await _context.Items.FindAsync(uniqueId.Id);
        if (item == null)
        {
            throw new NotFoundException();
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Items
    /// </summary>
    public async Task<List<Item>> Items(ItemFindManyArgs findManyArgs)
    {
        var items = await _context
            .Items.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return items.ConvertAll(item => item.ToDto());
    }

    /// <summary>
    /// Meta data about Item records
    /// </summary>
    public async Task<MetadataDto> ItemsMeta(ItemFindManyArgs findManyArgs)
    {
        var count = await _context.Items.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Item
    /// </summary>
    public async Task<Item> Item(ItemWhereUniqueInput uniqueId)
    {
        var items = await this.Items(
            new ItemFindManyArgs { Where = new ItemWhereInput { Id = uniqueId.Id } }
        );
        var item = items.FirstOrDefault();
        if (item == null)
        {
            throw new NotFoundException();
        }

        return item;
    }

    /// <summary>
    /// Update one Item
    /// </summary>
    public async Task UpdateItem(ItemWhereUniqueInput uniqueId, ItemUpdateInput updateDto)
    {
        var item = updateDto.ToModel(uniqueId);

        _context.Entry(item).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Items.Any(e => e.Id == item.Id))
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
