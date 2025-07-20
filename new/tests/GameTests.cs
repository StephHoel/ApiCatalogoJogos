using Domain.Entities;
using Xunit;

public class GameTests
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
}