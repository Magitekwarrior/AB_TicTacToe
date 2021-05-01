using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToeAPI.Infrastructure.Models;

namespace TicTacToeAPI.Service.Contract
{
  public interface IGameService
  {
    Task<IEnumerable<Game>> GetGamesHistory(string Player1Name);

    Task<Game> StartNewGame();

    Task<NextMove> PlayNextMove(PlayerMove move);
  }
}
