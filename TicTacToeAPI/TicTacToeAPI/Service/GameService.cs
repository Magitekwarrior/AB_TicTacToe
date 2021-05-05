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
        newMove.GameCompleted = (currentGame.Status != GameStatus.Incomplete.ToString());
        newMove.Winner = currentGame.Winner;

        if (!isPlayerWinner)
        {
          if (currentGame.Status == GameStatus.Incomplete.ToString())
          {
            var nextCpuMove = GetCpuMove(currentGame);
            newMove.Cell = nextCpuMove.Cell;
            newMove.Value = nextCpuMove.Value;

            // Save cpu player's move
            await _gameRepo.SaveMove(nextCpuMove);

            var isCpuWinner = IsPlayerWinner(currentGame, GamePlayer.Cpu);
            newMove.GameCompleted = (currentGame.Status != GameStatus.Incomplete.ToString());
            newMove.Winner = currentGame.Winner;
          }
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
      var isWinner = CheckWinCondition(currentGame);
      if (!isWinner)
      {
        var isIncomplete = currentGame.AnyEmptyCell();
        if (isIncomplete)
        {
          currentGame.SetGameStatus(GameStatus.Incomplete);
        }
        else
        {
          currentGame.SetGameStatus(GameStatus.Draw);
        }
      }
      else
      { // winner found.
        if (player == GamePlayer.Human)
        {
          currentGame.SetGameStatus(GameStatus.PlayerOneWon);
          currentGame.SetGameWinner(GamePlayer.Human.ToString());
        }
        else
        {
          currentGame.SetGameStatus(GameStatus.PlayerTwoWon);
          currentGame.SetGameWinner(GamePlayer.Cpu.ToString());
        }

      }

      return isWinner;
    }

    /// <summary>
    /// Check if the board has completed with a win condition
    /// </summary>
    /// <param name="currentGame"></param>
    /// <returns></returns>
    private bool CheckWinCondition(Game currentGame)
    {
      bool foundWinner = false;

      int[,] lines = new int[,]
         {
            {0,1,2},
            {3,4,5},
            {6,7,8},
            {0,3,6},
            {1,4,7},
            {2,5,8},
            {0,4,8},
            {2,4,6}
         };

      currentGame.GetBoardValues(out string[] board);

      var rowLength = lines.GetLength(0);
      for (int i = 0; i < rowLength; i++)
      {
        //var [a, b, c] = lines[i];
        var a = lines[i, 0];
        var b = lines[i, 1];
        var c = lines[i, 2];


        if ((!String.IsNullOrEmpty(board[a]) &&
            (board[a].ToUpper() == "X" || board[a].ToUpper() == "O")))
        {
          if (board[a] == board[b] && board[a] == board[c])
          {
            foundWinner = true;
          }
        }
      }

      var z = 0;

      return foundWinner;
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
