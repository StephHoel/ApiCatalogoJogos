using Domain.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IGameService
{
    Task<IEnumerable<GameDto>> GetAllAsync();

    Task<GameDto> GetByIdAsync(Guid id);

    Task<Game?> CreateAsync(GameDto game);

    Task UpdateAsync(GameDto dto);

    Task DeleteAsync(Guid id);
}