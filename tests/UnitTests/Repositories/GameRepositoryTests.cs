using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Repositories;

public class GameRepositoryTests
{
    private readonly DbContextOptions<GameCatalogDbContext> _dbOptions;
    private readonly GameRepository _repository;

    public GameRepositoryTests()
    {
        _dbOptions = new DbContextOptionsBuilder<GameCatalogDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _repository = new GameRepository(new(_dbOptions));
    }

    [Fact]
    public async Task AddAsync_ShouldAddGameAndReturnIt()
    {
        // Arrange
        var game = CreateGame();

        // Act
        var added = await _repository.AddAsync(game);

        // Assert
        Assert.NotNull(added);
        Assert.Equal(game.Id, added.Id);
        Assert.Equal(game.Title, added.Title);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnGame_WhenExists()
    {
        // Arrange
        var game = CreateGame();

        await _repository.AddAsync(game);

        // Act
        var found = await _repository.GetByIdAsync(game.Id);

        // Assert
        Assert.NotNull(found);
        Assert.Equal(game.Id, found.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
    {
        // Arrange

        // Act
        var result = await _repository.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnPaginatedGames()
    {
        // Arrange
        await _repository.AddAsync(new Game("Halo", "Bungie", 149.9m));
        await _repository.AddAsync(new Game("Zelda", "Nintendo", 199.9m));
        await _repository.AddAsync(new Game("FIFA", "EA", 299.9m));

        var (page, pageSize) = (1, 2);

        // Act
        var result = await _repository.GetAllAsync(page, pageSize);

        // Assert
        Assert.Equal(pageSize, result.Count());
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyExistingGame()
    {
        // Arrange
        var game = CreateGame();
        using (var context = CreateContext())
        {
            var repo = new GameRepository(context);

            await repo.AddAsync(game);
        }

        var newTitle = "Zelda: Tears of the Kingdom";

        // Act
        using (var context = CreateContext())
        {
            var repo = new GameRepository(context);

            var dto = new GameDto
            {
                Id = game.Id,
                Title = newTitle,
                Developer = game.Developer,
                Price = game.Price
            };

            await repo.UpdateAsync(dto);
        }

        // Assert
        using (var context = CreateContext())
        {
            var repo = new GameRepository(context);

            var updated = await repo.GetByIdAsync(game.Id);

            Assert.NotNull(updated);
            Assert.Equal(newTitle, updated!.Title);
        }
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveGame()
    {
        // Arrange
        var game = CreateGame();
        await _repository.AddAsync(game);

        // Act
        await _repository.DeleteAsync(game.Id);

        var deleted = await _repository.GetByIdAsync(game.Id);

        // Assert
        Assert.Null(deleted);
    }

    [Fact]
    public async Task DeleteAsync_ShouldNotThrow_WhenGameDoesNotExist()
    {
        // Arrange
        await _repository.AddAsync(CreateGame());

        // Act
        await _repository.DeleteAsync(Guid.NewGuid());
    }

    private GameCatalogDbContext CreateContext()
    {
        return new GameCatalogDbContext(_dbOptions);
    }

    private static Game CreateGame()
        => new GameDto
        {
            Id = Guid.NewGuid(),
            Title = "Zelda",
            Developer = "Nintendo",
            Price = 199.99m,
        };
}