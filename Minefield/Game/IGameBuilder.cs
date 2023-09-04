using MineField.Models;

namespace MineField.Game;

public interface IGameBuilder
{
    public (Board board, Player player) NewGame();
}