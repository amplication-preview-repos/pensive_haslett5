using Microsoft.AspNetCore.Mvc;

namespace GameBackendService.APIs;

[ApiController()]
public class ItemsController : ItemsControllerBase
{
    public ItemsController(IItemsService service)
        : base(service) { }
}
