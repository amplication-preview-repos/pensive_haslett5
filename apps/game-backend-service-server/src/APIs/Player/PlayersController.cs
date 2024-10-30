using Microsoft.AspNetCore.Mvc;

namespace GameBackendService.APIs;

[ApiController()]
public class PlayersController : PlayersControllerBase
{
    public PlayersController(IPlayersService service)
        : base(service) { }
}
