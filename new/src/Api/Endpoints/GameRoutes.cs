using Application.Interfaces;
using Domain.DTOs;

namespace Api.Endpoints;

public static class GameRoutes
{
    public static void GameEndpoints(this WebApplication app)
    {
        app.MapGet("/api/games", async (IGameService service) =>
        {
            var result = await service.GetAllAsync();
            return Results.Ok(result);
        })
            .WithName("GetAllGame")
            .Produces<IEnumerable<GameDto>>(StatusCodes.Status200OK)
            .WithSummary("Get all games");

        app.MapGet("/api/games/{id:guid}", async (IGameService service, Guid id) =>
        {
            var game = await service.GetByIdAsync(id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        })
            .WithName("GetGameById")
            .Produces<IEnumerable<GameDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Get a game by ID");

        app.MapPost("/api/games", async (IGameService service, GameDto dto) =>
        {
            var created = await service.CreateAsync(dto);
            return created is null
                ? Results.BadRequest()
                : Results.Created($"/api/games/{created.Id}", created);
        })
            .WithName("CreateGame")
            .Produces<GameDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Create new a game");

        app.MapPut("/api/games/{id:guid}", async (IGameService service, Guid id, GameDto dto) =>
        {
            if (id != dto.Id) return Results.BadRequest();
            await service.UpdateAsync(dto);
            return Results.NoContent();
        })
            .WithName("UpdateGame")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Update a game by ID");

        app.MapDelete("/api/games/{id:guid}", async (IGameService service, Guid id) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
            .WithName("DeleteGame")
            .Produces(StatusCodes.Status204NoContent)
            .WithSummary("Delete a game by ID");
    }
}