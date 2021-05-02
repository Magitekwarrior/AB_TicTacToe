using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToeAPI.Infrastructure.Models;

namespace TicTacToeAPI.Infrastructure.Repositories.Contracts
{
  public interface IGameRepo
  {
    Task<IEnumerable<Game>> GetPlayersGames(string PlayerName);

    Task<Game> GetGame(Guid GameId);

    Task<Game> CreateGame(string PlayerName);

    Task SaveMove(PlayerMove Move);
  }
}
