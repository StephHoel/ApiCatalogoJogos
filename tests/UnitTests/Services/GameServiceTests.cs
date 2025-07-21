using Application.Services;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace UnitTests.Services;

public class GameServiceTests
{
    private readonly Mock<IGameRepository> _repositoryMock;
    private readonly GameService _service;

    public GameServiceTests()
    {
        _repositoryMock = new Mock<IGameRepository>();
        _service = new GameService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnGameDtos_WhenGamesExist()
    {
        // Arrange
        var games = new List<Game>
        {
            new("Halo", "Bungie", 149.9m),
            new("Zelda", "Nintendo", 199.9m)
        };

        _repositoryMock.Setup(r => r.GetAllAsync(1, 10)).ReturnsAsync(games);

        // Act
        var result = await _service.GetAllAsync(1, 10);

        // Assert
        Assert.Equal(games.Count, result.Count());
        Assert.Equal(games.First().Title, result.First().Title);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoGames()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync([]);

        // Act
        var result = await _service.GetAllAsync(1, 10);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnGameDto_WhenGameExists()
    {
        // Arrange
        var dto = CreateGameDto();

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dto);

        // Act
        var result = await _service.GetByIdAsync(dto.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dto.Id, result!.Id);
        Assert.Equal(dto.Title, result.Title);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenGameNotFound()
    {
        // Arrange
        var id = CreateGameDto().Id;

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Game?)null);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedGame_WhenValid()
    {
        // Arrange
        var dto = CreateGameDto();

        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Game>())).ReturnsAsync(dto);

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dto.Title, result.Title);
    }

    [Fact]
    public async Task UpdateAsync_ShouldCallRepositoryUpdate()
    {
        // Arrange
        var dto = CreateGameDto();

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Game>()));

        // Act
        await _service.UpdateAsync(dto);

        // Assert
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Game>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallRepositoryDelete()
    {
        // Arrange
        var id = CreateGameDto().Id;

        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Guid>()));

        // Act
        await _service.DeleteAsync(id);

        // Assert
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Once);
    }

    private static GameDto CreateGameDto()
        => new() { Id = Guid.NewGuid(), Title = "Halo", Developer = "Bungie", Price = 149.9m };
}