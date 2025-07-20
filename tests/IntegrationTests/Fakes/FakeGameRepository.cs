using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace IntegrationTests.Fakes;

public class FakeGameRepository : IGameRepository
{
    public List<Game> Games { get; set; } = [
        new GameDto {
            Id = Guid.Parse("0ee5df8e-cc13-454f-bd8b-daf7c1953b37"),
            Title = "Halo",
            Developer = "Bungie",
            Price = 149.99m,
        }
   ];

    public async Task<Game?> AddAsync(Game game)
    {
        Games.Add(game);

        return game;
    }

    public async Task DeleteAsync(Guid id)
    {
        var game = Games.FirstOrDefault(g => g.Id == id);

        if (game != null) Games.Remove(game);
    }

    public async Task<IEnumerable<Game>> GetAllAsync(int page, int pageSize)
    {
        var paginated = Games.Skip((page - 1) * pageSize).Take(pageSize);

        return paginated;
    }

    public async Task<Game?> GetByIdAsync(Guid id)
        => Games.FirstOrDefault(g => g.Id == id);

    public async Task UpdateAsync(Game game)
    {
        var index = Games.FindIndex(g => g.Id == game.Id);

        if (index >= 0) Games[index] = game;
    }
}