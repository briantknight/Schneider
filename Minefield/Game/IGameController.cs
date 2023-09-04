using MineField.Models;

namespace MineField.Game;

public interface IGameController
{
    public MoveResult Move(Direction direction);
}