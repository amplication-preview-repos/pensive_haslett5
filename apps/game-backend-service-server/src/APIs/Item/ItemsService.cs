using GameBackendService.Infrastructure;

namespace GameBackendService.APIs;

public class ItemsService : ItemsServiceBase
{
    public ItemsService(GameBackendServiceDbContext context)
        : base(context) { }
}
