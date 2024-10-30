using GameBackendService.Infrastructure;

namespace GameBackendService.APIs;

public class CharactersService : CharactersServiceBase
{
    public CharactersService(GameBackendServiceDbContext context)
        : base(context) { }
}
