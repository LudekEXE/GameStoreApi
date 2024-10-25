using GameStore.Api.Data;
using GameStore.Api.Dtos.PlayerOperationsDto;
using GameStore.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Endpoints;

public static class PlayerEndpoints
{
    const string GetGameEndpoint = "GetPlayer";
    
    private static readonly List<PlayerDto> Players =
    [
        new(
            1,
            "email@gmail.com",
            "Nickname"
        ),
        new(
            2,
            "email1@gmail.com",
            "Nickname1"
        ),
        new(
        3,
        "email2@gmail.com",
        "Nickname2"
        )
    ];

    public static RouteGroupBuilder MapPlayerEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("players")
            .WithParameterValidation();
        
        //GET /players (display all games)
        group.MapGet("/", () => Players);
        
        //GET /players/{id} (display indicated game)
        group.MapGet("/{id}", (int id) =>
        {
            PlayerDto? player = Players.Find(p => p.Id == id);
            return player is null ? Results.NotFound() : Results.Ok(player);
        })
        .WithName(GetGameEndpoint);
        
        //POST /players (adding player)
        group.MapPost("/", (
                [FromBody] CreatePlayerDto newPlayer,
                [FromServices] PlayerContext dbContext) =>
                {
                    Player player = new()
                    {
                        Emial = newPlayer.Email,
                        Nick = newPlayer.Nick,
                        Password = newPlayer.Password
                    };
                    dbContext.Players.Add(player);
                    dbContext.SaveChanges();
                    
                    PlayerDto playerDto = new
                        (
                            player.Id,
                            player.Emial,
                            player.Nick
                        );
                    return Results.CreatedAtRoute(GetGameEndpoint, new { id = playerDto.Id }, playerDto);
                });
        //DELETE /players (deleting indiceted game)

        group.MapDelete("/{id}", (int id) =>
        {
            Players.RemoveAll(player => player.Id == id);
            return Results.NoContent();
        });
        return group;
    }
}