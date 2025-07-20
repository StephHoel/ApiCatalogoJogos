using Application.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class GameService : IGameService
{
    private readonly IGameRepository _repository;

    public GameService(IGameRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GameDto>> GetAllAsync()
    {
        var games = await _repository.GetAllAsync();
        return games.Select(g => new GameDto
        {
            Id = g.Id,
            Title = g.Title,
            Developer = g.Developer,
            Price = g.Price
        });
    }

    public async Task<GameDto?> GetByIdAsync(Guid id)
    {
        var game = await _repository.GetByIdAsync(id);
        if (game == null) return null;

        return new GameDto
        {
            Id = game.Id,
            Title = game.Title,
            Developer = game.Developer,
            Price = game.Price
        };
    }

    public async Task<Game?> CreateAsync(GameDto dto)
    {
        return await _repository.AddAsync(dto);
    }

    public async Task UpdateAsync(GameDto dto)
    {
        await _repository.UpdateAsync(dto);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}