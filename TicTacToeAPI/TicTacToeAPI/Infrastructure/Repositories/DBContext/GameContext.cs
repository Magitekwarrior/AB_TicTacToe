using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Infrastructure.Models;

namespace TicTacToeAPI.Infrastructure.Repositories.DBContext
{
  public class GameContext: DbContext
  {
    public GameContext(DbContextOptions<GameContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Game>()
        .ToTable("tictactoegame")
        .HasKey(x => x.Id);
    }

    public DbSet<Game> TicTacToeGames { get; set; }
  }
}
