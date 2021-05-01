using Microsoft.EntityFrameworkCore;

namespace TicTacToeAPI.Infrastructure.Repositories.DBContext
{
  public class GameContext: DbContext
  {
    public GameContext(DbContextOptions<GameContext> options) : base(options)
    {

    }
  }
}
