using GameBackendService.APIs.Common;
using GameBackendService.APIs.Dtos;

namespace GameBackendService.APIs;

public interface IItemsService
{
    /// <summary>
    /// Create one Item
    /// </summary>
    public Task<Item> CreateItem(ItemCreateInput item);

    /// <summary>
    /// Delete one Item
    /// </summary>
    public Task DeleteItem(ItemWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Items
    /// </summary>
    public Task<List<Item>> Items(ItemFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Item records
    /// </summary>
    public Task<MetadataDto> ItemsMeta(ItemFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Item
    /// </summary>
    public Task<Item> Item(ItemWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Item
    /// </summary>
    public Task UpdateItem(ItemWhereUniqueInput uniqueId, ItemUpdateInput updateDto);
}
