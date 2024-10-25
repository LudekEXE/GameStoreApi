using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connSting = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connSting);
builder.Services.AddSqlite<PlayerContext>(connSting);

var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenres();

app.MapPlayerEndpoints();

await app.MigrateDbAsync();

app.Run();