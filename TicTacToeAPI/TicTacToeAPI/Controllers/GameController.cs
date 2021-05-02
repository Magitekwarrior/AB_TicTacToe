using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicTacToeAPI.Infrastructure.Models;
using TicTacToeAPI.Service.Contract;

namespace TicTacToeAPI.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class GameController : ControllerBase
  {
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
      _gameService = gameService;
    }

    /// <summary>
    /// Get History of games played by the specified player
    /// </summary>
    /// <param name="PlayerName"></param>
    /// <returns></returns>
    [HttpGet("history")]
    public async Task<IEnumerable<Game>> History(string PlayerName)
    {
      var history = await _gameService.GetGamesHistory(PlayerName);
      return history;
    }

    /// <summary>
    /// Start a new game or reset the current game
    /// </summary>
    /// <param name="PlayerName"></param>
    /// <returns></returns>
    [HttpPost("start")]
    public async Task<Game> StartGame(string PlayerName)
    {
      var newGame = await _gameService.StartNewGame(PlayerName);
      return newGame;
    }

    /// <summary>
    /// Save Human player's most recent move ('X') and the Cpu player's next move ('O')
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    [HttpPost("{id}/play-move")]
    public async Task<NextMove> PlayMove(PlayerMove move)
    {
      var nextMove = await _gameService.PlayNextMove(move);
      return nextMove;
    }

  }
}

