using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using Xunit;

namespace UnitTests.Services;

public class GameServicesTests
{
    [Fact]
    public void Should_Update_Valid_Price()
    {
        var game = new Game("Test Game", "Dev Studio", 10);
        game.UpdatePrice(20);
        Assert.Equal(20, game.Price);
    }

    [Fact]
    public void Should_Throw_Exception_For_Invalid_Price()
    {
        var game = new Game("Test Game", "Dev Studio", 10);
        Assert.Throws<ArgumentException>(() => game.UpdatePrice(0));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfGameDto()
    {
        // Arrange
        var repoMock = new Mock<IGameRepository>();
        repoMock.Setup(r => r.GetAllAsync(1, 2)).ReturnsAsync(new List<Game>
        {
            new("Zelda", "Nintendo", 199),
            new("Halo", "Bungie", 149)
        });

        var service = new GameService(repoMock.Object);

        // Act
        var result = await service.GetAllAsync(1, 2);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal("Zelda", result.First().Title);
    }
}