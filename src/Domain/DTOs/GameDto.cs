using Domain.Entities;

namespace Domain.DTOs;

public class GameDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Developer { get; set; }
    public decimal Price { get; set; }

    public static implicit operator GameDto(Game game)
    {
        return new GameDto()
        {
            Id = game.Id,
            Title = game.Title,
            Developer = game.Developer,
            Price = game.Price,
        };
    }
}