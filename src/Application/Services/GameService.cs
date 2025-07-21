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

    public async Task<IEnumerable<GameDto>> GetAllAsync(int page, int pageSize)
    {
        var games = await _repository.GetAllAsync(page, pageSize);

        return games.Select(game => (GameDto)game);
    }

    public async Task<GameDto?> GetByIdAsync(Guid id)
    {
        var game = await _repository.GetByIdAsync(id);

        if (game is null) return null;

        return game;
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