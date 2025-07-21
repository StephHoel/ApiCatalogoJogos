using Domain.DTOs;
using IntegrationTests.Factory;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace IntegrationTests.Tests;

public class GameRoutesTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;

    public GameRoutesTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetById_ShouldReturnGame_WhenExists()
    {
        // Arrange
        var game = _factory.FakeRepo.Games.First();

        // Act
        var response = await _client.GetAsync($"/api/games/{game.Id}");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var returned = await response.Content.ReadFromJsonAsync<GameDto>();
        Assert.Equal(game.Id, returned!.Id);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenGameDoesNotExist()
    {
        // Act
        var response = await _client.GetAsync($"/api/games/{Guid.NewGuid()}");

        // Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ShouldReturnPaginatedGames_WhenExists()
    {
        // Arrange
        _factory.FakeRepo.Games.AddRange(
        [
            new GameDto { Id = Guid.NewGuid(), Title = "Halo", Developer = "Bungie", Price = 149.9m },
            new GameDto { Id = Guid.NewGuid(), Title = "Zelda", Developer = "Nintendo", Price = 199.9m }
        ]);

        // Act
        var response = await _client.GetAsync("/api/games?page=1&pageSize=2");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<GameDto>>();
        Assert.NotNull(result);
        Assert.Equal(2, result!.Count());
    }

    [Fact]
    public async Task GetAll_ShouldReturnNoContent_WhenNoGames()
    {
        // Act
        var response = await _client.GetAsync("/api/games?page=2&pageSize=5");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task CreateGame_ShouldReturnCreated_WhenValid()
    {
        // Arrange
        var dto = new GameDto
        {
            Id = Guid.NewGuid(),
            Title = "New Game",
            Developer = "Dev Studio",
            Price = 99.9m
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/games", dto);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var created = await response.Content.ReadFromJsonAsync<GameDto>();
        Assert.Equal(dto.Title, created!.Title);
    }

    [Fact]
    public async Task UpdateGame_ShouldReturnNoContent_WhenIdsMatch()
    {
        // Arrange
        var game = new GameDto
        {
            Id = Guid.NewGuid(),
            Title = "Original",
            Developer = "Studio",
            Price = 149.9m
        };

        _factory.FakeRepo.Games.Add(game);

        game.Title = "Updated";

        // Act
        var response = await _client.PutAsJsonAsync($"/api/games/{game.Id}", game);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task UpdateGame_ShouldReturnBadRequest_WhenIdsDoNotMatch()
    {
        // Arrange
        var dto = new GameDto
        {
            Id = Guid.NewGuid(),
            Title = "Mismatch",
            Developer = "Dev",
            Price = 120
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/api/games/{Guid.NewGuid()}", dto);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task DeleteGame_ShouldReturnNoContent_WhenExists()
    {
        // Arrange
        var game = _factory.FakeRepo.Games.First();

        // Act
        var response = await _client.DeleteAsync($"/api/games/{game.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}