using Application.Interfaces;
using Domain.Common;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class GameRoutes
{
    public static void GameEndpoints(this WebApplication app)
    {
        app.MapGet("/api/games", async (IGameService service,
                                        [FromQuery] int page = 1,
                                        [FromQuery] int pageSize = 5) =>
        {
            var games = await service.GetAllAsync(page, pageSize);
            return games.Any() ? Results.Ok(games) : Results.NoContent();
        })
            .WithName("GetGames")
            .WithSummary("Get paginated list of games")
            .WithDescription("Returns a paginated list of games. Pagination is required. The page must be 1 or greater, and page size must be between 1 and 50.")
            .Produces<IEnumerable<GameDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        app.MapGet("/api/games/{id:guid}", async (IGameService service, Guid id) =>
        {
            var game = await service.GetByIdAsync(id);
            return game is null ? Results.NotFound(ErrorMessages.Game.NotFound) : Results.Ok(game);
        })
            .WithName("GetGameById")
            .WithSummary("Get a game by ID")
            .WithDescription("Returns the game for the specified ID, or a 404 if not found.")
            .Produces<GameDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound);

        app.MapPost("/api/games", async (IGameService service, GameDto dto) =>
        {
            var created = await service.CreateAsync(dto);
            return created is null
                ? Results.BadRequest(ErrorMessages.Game.CreateError)
                : Results.Created($"/api/games/{created.Id}", created);
        })
            .WithName("CreateGame")
            .WithSummary("Create a new game")
            .WithDescription("Creates a new game and returns its representation with location header.")
            .Produces<GameDto>(StatusCodes.Status201Created)
            // .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status400BadRequest);

        app.MapPut("/api/games/{id:guid}", async (IGameService service, Guid id, GameDto dto) =>
        {
            if (id != dto.Id)
                return Results.BadRequest(ErrorMessages.Game.UnmachId);

            await service.UpdateAsync(dto);
            return Results.NoContent();
        })
            .WithName("UpdateGame")
            .WithSummary("Update an existing game")
            .WithDescription("Updates a game by ID. The ID in the route must match the ID in the request body.")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        app.MapDelete("/api/games/{id:guid}", async (IGameService service, Guid id) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        })
            .WithName("DeleteGame")
            .WithSummary("Delete a game by ID")
            .WithDescription("Deletes the game with the specified ID.")
            .Produces(StatusCodes.Status204NoContent);
    }
}