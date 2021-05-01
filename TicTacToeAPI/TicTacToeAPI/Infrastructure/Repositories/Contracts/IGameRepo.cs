using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToeAPI.Infrastructure.Models;

namespace TicTacToeAPI.Infrastructure.Repositories.Contracts
{
  public interface IGameRepo
  {
    Task<IEnumerable<Game>> GetPlayersGames(string Player1Name);

    Task<Game> CreateGame();

    Task SaveMove(PlayerMove move);
  }
}
