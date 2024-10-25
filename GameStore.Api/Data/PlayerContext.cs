using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public class PlayerContext(DbContextOptions<PlayerContext> options) : DbContext(options)
{
    public DbSet<Player> Players => Set<Player>();
}