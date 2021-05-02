using System;
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

    /// <summary>
    /// Get History of games played by the specified player
    /// </summary>
    /// <param name="PlayerName"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Game>> GetGamesHistory(string PlayerName)
    {
      var history = await _gameRepo.GetPlayersGames(PlayerName);
      return history;
    }

    /// <summary>
    /// Save Human player's most recent move ('X') and the Cpu player's next move ('O')
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    public async Task<NextMove> PlayNextMove(PlayerMove move)
    {
      var newMove = new NextMove();

      try
      {
        // Save human player's move
        await _gameRepo.SaveMove(move);

        // Get saved board including player's latest move
        var currentGame = await _gameRepo.GetGame(move.GameId);
        if (IsGameComplete(currentGame))
        {
          var nextCpuMove = GetCpuMove(currentGame);

          // Save cpu player's move
          await _gameRepo.SaveMove(nextCpuMove);

          newMove.Cell = nextCpuMove.Cell;
          newMove.Value = nextCpuMove.Value;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }

      return newMove;
    }

    private bool IsGameComplete(Game currentGame)
    {
      // TODO: determine if the game is won or not
      // TODO: determine if there is a need for the game to continue depending on how many cells are left and if a win is possible

      Random randomNo = new Random();
      if (randomNo.Next(100) < 20)
      {// will be true 20% of the time
        return false;
      }

      return true;
    }

    /// <summary>
    /// Pick a random cell that is not already filled
    /// </summary>
    /// <param name="currentGame"></param>
    private PlayerMove GetCpuMove(Game currentGame)
    {
      Random rng = new Random();
      var cell = rng.Next(1, 9);

      while (currentGame.CellHasValue(cell))
      {
        cell = rng.Next(1, 9);
      }

      var cpuMove = new PlayerMove()
      {
        GameId = currentGame.Id,
        Cell = cell,
        Value = "O" // CPU will be 'O' and human player will be 'X'
      };

      return cpuMove;
    }

    /// <summary>
    /// Start a new game or reset a game.
    /// </summary>
    /// <param name="PlayerName"></param>
    /// <returns></returns>
    public async Task<Game> StartNewGame(string PlayerName)
    {
      var newGame = await _gameRepo.CreateGame(PlayerName);
      return newGame;
    }
  }
}
