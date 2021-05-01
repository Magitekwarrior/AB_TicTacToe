using System;

namespace TicTacToeAPI.Infrastructure.Models
{
  public class PlayerMove
  {
    public Guid GameId { get; set; }
    public int Cell { get; set; }
    public string Value { get; set; }
  }
}
