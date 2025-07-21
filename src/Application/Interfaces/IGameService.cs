using Domain.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IGameService
{
    Task<IEnumerable<GameDto>> GetAllAsync(int page, int pageSize);

    Task<GameDto?> GetByIdAsync(Guid id);

    Task<Game?> CreateAsync(GameDto dto);

    Task UpdateAsync(GameDto dto);

    Task DeleteAsync(Guid id);
}