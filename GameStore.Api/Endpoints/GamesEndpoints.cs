using GameStore.Api.Data;
using GameStore.Api.Dtos.GamesOpertionsDto;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpoint = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
                        .WithParameterValidation();
        
        // GET /games (display all games)
        group.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Games
                .Include(game => game.Genre)
                .Select(game => game.ToGameSummaryDto())
                .AsNoTracking()
                .ToListAsync()
            );

// GET /games/[id] (display indicated game)
        group.MapGet("/{id}", async (int id , GameStoreContext dbContext) =>
            {
                Game? game = await dbContext.Games.FindAsync(id);
        
                return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
            })
            .WithName(GetGameEndpoint);

// POST /games (adding game)
        group.MapPost("/", async (
            [FromBody] CreateGameDto newGame, 
           [FromServices] GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            dbContext.Add(game);
            await dbContext.SaveChangesAsync();
            
            return Results.CreatedAtRoute(GetGameEndpoint, new { id = game.Id }, game.ToGameDetailsDto());
        });

//PUT /games/[id] (editing indicated game)
        group.MapPut("/{id}", async (int id, UpdateGameDto updateGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }
    
            dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updateGame.ToEntity(id));
            
            await dbContext.SaveChangesAsync();
            
            return Results.NoContent();
        });

//DELETE /games/[id] (deleting indiceted game)

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
    //batch delete (naucz sie)
            await dbContext.Games
                .Where(game => game.Id == id)
                .ExecuteDeleteAsync();
    
            return Results.NoContent();
        });
        return group;
    }
}