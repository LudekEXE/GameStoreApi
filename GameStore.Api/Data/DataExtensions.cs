using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        var dbcontext1 = scope.ServiceProvider.GetRequiredService<PlayerContext>();
        await dbContext.Database.MigrateAsync();
        dbcontext1.Database.Migrate();
    }
    
}