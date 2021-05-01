using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToeAPI.Infrastructure.Models;
using TicTacToeAPI.Infrastructure.Repositories.Contracts;
using TicTacToeAPI.Service.Contract;

namespace TicTacToeAPI.Service
{
  public class GameService : IGameService
  {
    private readonly IGameRepo _gameRepo;

    public GameService(IGameRepo gameRepo)
    {
      _gameRepo = gameRepo;
    }

    public async Task<IEnumerable<Game>> GetGamesHistory(string Player1Name)
    {
      var history = new List<Game>();

      return history;
    }

    public async Task<NextMove> PlayNextMove(PlayerMove move)
    {
      var newMove = new NextMove();

      return newMove;
    }

    public async Task<Game> StartNewGame()
    {
      var newGame = new Game();

      return newGame;
    }
  }
}
