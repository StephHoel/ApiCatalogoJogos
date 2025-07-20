using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class GameCatalogDbContext(DbContextOptions<GameCatalogDbContext> options)
    : DbContext(options)
{
    public DbSet<Game> Games { get; set; }
}