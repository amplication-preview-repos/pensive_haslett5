using GameBackendService.Infrastructure;

namespace GameBackendService.APIs;

public class QuestsService : QuestsServiceBase
{
    public QuestsService(GameBackendServiceDbContext context)
        : base(context) { }
}
