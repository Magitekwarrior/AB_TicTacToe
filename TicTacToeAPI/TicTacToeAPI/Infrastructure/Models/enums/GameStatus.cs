using System.Runtime.Serialization;

namespace TicTacToeAPI.Infrastructure.Models.enums
{
  public enum GameStatus
  {
    [EnumMember(Value = "Incomplete")]
    Incomplete,
    [EnumMember(Value = "PlayerOneWon")]
    PlayerOneWon,
    [EnumMember(Value = "PlayerTwoWon")]
    PlayerTwoWon,
    [EnumMember(Value = "Draw")]
    Draw
  }
}
