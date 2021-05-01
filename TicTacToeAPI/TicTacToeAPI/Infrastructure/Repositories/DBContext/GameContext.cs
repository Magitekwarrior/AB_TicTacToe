using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Infrastructure.Models;

namespace TicTacToeAPI.Infrastructure.Repositories.DBContext
{
  public class GameContext: DbContext
  {
    public GameContext(DbContextOptions<GameContext> options) : base(options)
    {

    }

    public DbSet<Game> TicTacToeGame { get; set; }
  }
}
