using GameBackendService.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameBackendService.Infrastructure;

public class GameBackendServiceDbContext : IdentityDbContext<IdentityUser>
{
    public GameBackendServiceDbContext(DbContextOptions<GameBackendServiceDbContext> options)
        : base(options) { }

    public DbSet<CharacterDbModel> Characters { get; set; }

    public DbSet<PlayerDbModel> Players { get; set; }

    public DbSet<QuestDbModel> Quests { get; set; }

    public DbSet<ItemDbModel> Items { get; set; }
}
