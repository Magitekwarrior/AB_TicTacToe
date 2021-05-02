using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Game> CreateGame(string PlayerName)
    {
      var game = new Game()
      {
        Player1Name = PlayerName,
        Player2Name = "CPU",
        GameStartDate = DateTime.Now
      };

      await _dbContext.TicTacToeGames.AddAsync(game);
      await _dbContext.SaveChangesAsync();

      return await Task.FromResult(game);
    }

    public async Task<IEnumerable<Game>> GetPlayersGames(string PlayerName)
    {
      var history = _dbContext.TicTacToeGames
        .Where(i => i.Player1Name.ToUpper() == PlayerName.ToUpper())
        .ToList();

      return history;
    }

    public async Task<Game> GetGame(Guid GameId)
    {
      var game = await _dbContext.TicTacToeGames.FirstOrDefaultAsync(g => g.Id.Equals(GameId));
      if (game == null) 
        throw new ApplicationException("Game not found");

      return game;
    }

    public async Task SaveMove(PlayerMove move)
    {
      var game = await _dbContext.TicTacToeGames.FirstOrDefaultAsync(g => g.Id.Equals(move.GameId));
      if (game == null)
        throw new ApplicationException("Game not found");

      if (move.Cell == 0 || move.Cell > 9) 
        throw new ApplicationException("Cell is not valid");

      game.SetCell(move.Cell, move.Value);
      _dbContext.TicTacToeGames.Update(game);
    }
  }
}
