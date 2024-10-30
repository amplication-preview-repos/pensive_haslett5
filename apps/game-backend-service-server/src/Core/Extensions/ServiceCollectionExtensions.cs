using GameBackendService.APIs;

namespace GameBackendService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICharactersService, CharactersService>();
        services.AddScoped<IItemsService, ItemsService>();
        services.AddScoped<IPlayersService, PlayersService>();
        services.AddScoped<IQuestsService, QuestsService>();
    }
}
