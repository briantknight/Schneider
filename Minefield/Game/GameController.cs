using MineField.Models;

namespace MineField.Game;

public class GameController : IGameController

{
    private readonly GameOptions _gameOptions;
    private readonly Board _board;
    private readonly Player _player;

    public GameController(IGameBuilder builder, GameOptions gameOptions)
    {
        _gameOptions = gameOptions;
        var game = builder.NewGame();
        _board = game.board;
        _player = game.player;
    }

    public MoveResult Move(Direction direction)
    {
        // Moves player and guards board edges
        return direction switch
        {
            Direction.Up when _player.CurrentLocation.Row > 0 => PlayMove(Direction.Up),
            Direction.Down when _player.CurrentLocation.Row < _gameOptions.NumberOfRows => PlayMove(Direction.Down),
            Direction.Left when _player.CurrentLocation.Column > 0 => PlayMove(Direction.Left),
            Direction.Right when _player.CurrentLocation.Column < _gameOptions.NumberOfColumns => PlayMove(Direction.Right),
            _ => _player.VoidMove()
        };
    }

    private MoveResult PlayMove(Direction direction)
    {
        _player.IncrementMoves();

        var desiredLocation = direction switch 
        {
            Direction.Up => Location.MoveUp(_player.CurrentLocation),
            Direction.Down => Location.MoveDown(_player.CurrentLocation),
            Direction.Left=> Location.MoveLeft(_player.CurrentLocation),
            Direction.Right => Location.MoveRight(_player.CurrentLocation),
            _ => throw new NotImplementedException()
        };

        return IsHit(desiredLocation)
            ? _player.FailedMove()
            : _player.SuccessMove(desiredLocation, _board);
    }

    public bool IsHit(Location location)
    {
        return _board.MineLocations.Any(loc => loc == location);
    }
}