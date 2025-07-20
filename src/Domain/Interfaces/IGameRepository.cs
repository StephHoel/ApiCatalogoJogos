namespace Domain.Interfaces;

using Domain.Entities;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetAllAsync(int page, int pageSize);

    Task<Game?> GetByIdAsync(Guid id);

    Task<Game?> AddAsync(Game game);

    Task UpdateAsync(Game game);

    Task DeleteAsync(Guid id);
}