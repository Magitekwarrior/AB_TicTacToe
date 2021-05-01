using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToeAPI.Infrastructure.Models;
using TicTacToeAPI.Infrastructure.Repositories.Contracts;
using TicTacToeAPI.Infrastructure.Repositories.DBContext;

namespace TicTacToeAPI.Infrastructure.Repositories
{
  public class GameRepo : IGameRepo
  {
    private readonly GameContext _dbContext;

    public GameRepo(GameContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Game> CreateGame()
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<Game>> GetPlayersGames(string Player1Name)
    {
      throw new NotImplementedException();
    }

    public async Task SaveMove(PlayerMove move)
    {
      throw new NotImplementedException();
    }
  }
}
