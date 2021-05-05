using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToeAPI.Infrastructure.Models;
using TicTacToeAPI.Infrastructure.Models.enums;
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

        var isPlayerWinner = IsPlayerWinner(currentGame, GamePlayer.Human);
        if (!isPlayerWinner)
        {
          newMove.GameCompleted = (currentGame.Status != GameStatus.Incomplete.ToString());
          newMove.Winner = currentGame.Winner;
        }
        
        if (!newMove.GameCompleted)
        {
          var nextCpuMove = GetCpuMove(currentGame);
          // Save cpu player's move
          await _gameRepo.SaveMove(nextCpuMove);

          newMove.Cell = nextCpuMove.Cell;
          newMove.Value = nextCpuMove.Value;

          var isCpuWinner = IsPlayerWinner(currentGame, GamePlayer.Cpu);
        }
        else
        {
          currentGame.Status = GameStatus.Incomplete.ToString();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }

      return newMove;
    }

    /// <summary>
    /// Check if the player has won the game and update game status 
    /// </summary>
    /// <param name="currentGame"></param>
    /// <param name="player"></param>
    /// <returns></returns>
    private bool IsPlayerWinner(Game currentGame, GamePlayer player)
    {
      var gameComplete = false;
      var isWinner = CheckWinCondition(currentGame);
      if (!isWinner)
      {
        var isIncomplete = currentGame.AnyEmptyCell();
        if (isIncomplete)
        {
          gameComplete = false;
          currentGame.Status = GameStatus.Incomplete.ToString();
        }
        else
        {
          gameComplete = true;
          currentGame.Status = GameStatus.Draw.ToString();
        }
      }
      else
      { // winner found.
        gameComplete = true;
        if (player == GamePlayer.Human)
        {
          currentGame.Status = GameStatus.PlayerOneWon.ToString();
          currentGame.Winner = GamePlayer.Human.ToString();
        }
        else
        {
          currentGame.Status = GameStatus.PlayerTwoWon.ToString();
          currentGame.Winner = GamePlayer.Cpu.ToString();
        }
          
      }

      return gameComplete;
    }

    /// <summary>
    /// Check if the board has completed with a win condition
    /// </summary>
    /// <param name="currentGame"></param>
    /// <returns></returns>
    private bool CheckWinCondition(Game currentGame)
    {
      currentGame.GetBoardValues(out string[] board);

      var isWinner =
        board[0] == board[1] && board[1] == board[2] ||
        board[3] == board[4] && board[4] == board[5] ||
        board[6] == board[7] && board[7] == board[8] ||
        board[0] == board[3] && board[3] == board[6] ||
        board[1] == board[4] && board[4] == board[7] ||
        board[2] == board[5] && board[5] == board[8] ||
        board[0] == board[4] && board[4] == board[8] ||
        board[2] == board[4] && board[4] == board[6];

      return isWinner;
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
