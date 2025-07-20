using Domain.DTOs;

namespace Domain.Entities;

public class Game
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Developer { get; private set; }
    public decimal Price { get; private set; }

    public Game(string title, string developer, decimal price)
    {
        Id = Guid.NewGuid();
        Title = title;
        Developer = developer;
        Price = price;
    }

    public static implicit operator Game(GameDto dto)
    {
        return new Game(dto.Title, dto.Developer, dto.Price)
        {
            Id = dto.Id,
        };
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0) throw new ArgumentException("Invalid price");
        Price = newPrice;
    }
}