using GameBackendService.Infrastructure;

namespace GameBackendService.APIs;

public class PlayersService : PlayersServiceBase
{
    public PlayersService(GameBackendServiceDbContext context)
        : base(context) { }
}
