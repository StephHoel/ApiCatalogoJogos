namespace Domain.DTOs;

public class GameDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Developer { get; set; }
    public decimal Price { get; set; }
}