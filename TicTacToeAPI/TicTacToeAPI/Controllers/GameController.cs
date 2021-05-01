using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicTacToeAPI.Infrastructure.Models;
using TicTacToeAPI.Service.Contract;

namespace TicTacToeAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GameController : ControllerBase
  {
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
      _gameService = gameService;
    }

    [HttpGet("history")]
    public async Task<IEnumerable<Game>> History(string PlayerName)
    {
      var history = await _gameService.GetGamesHistory(PlayerName);
      return history;
    }

    [HttpPost("start")]
    public async Task<Game> StartGame()
    {
      var newGame = await _gameService.StartNewGame();
      return newGame;
    }

    [HttpPost("{id}/play-move")]
    public async Task<NextMove> PlayMove(PlayerMove move)
    {
      var nextMove = await _gameService.PlayNextMove(move);
      return nextMove;
    }

  }
}

