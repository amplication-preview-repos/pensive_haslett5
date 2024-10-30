using Microsoft.AspNetCore.Mvc;

namespace GameBackendService.APIs;

[ApiController()]
public class QuestsController : QuestsControllerBase
{
    public QuestsController(IQuestsService service)
        : base(service) { }
}
