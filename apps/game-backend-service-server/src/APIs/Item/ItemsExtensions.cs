using GameBackendService.APIs.Dtos;
using GameBackendService.Infrastructure.Models;

namespace GameBackendService.APIs.Extensions;

public static class ItemsExtensions
{
    public static Item ToDto(this ItemDbModel model)
    {
        return new Item
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            ItemName = model.ItemName,
            Rarity = model.Rarity,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ItemDbModel ToModel(this ItemUpdateInput updateDto, ItemWhereUniqueInput uniqueId)
    {
        var item = new ItemDbModel
        {
            Id = uniqueId.Id,
            ItemName = updateDto.ItemName,
            Rarity = updateDto.Rarity
        };

        if (updateDto.CreatedAt != null)
        {
            item.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            item.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return item;
    }
}
