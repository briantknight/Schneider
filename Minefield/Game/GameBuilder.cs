using MineField.Models;

namespace MineField.Game;

public class GameBuilder : IGameBuilder
{
    private readonly GameOptions _gameOptions;

    public GameBuilder(GameOptions gameOptions)
    {
        _gameOptions = gameOptions;
    }

    public (Board board, Player player) NewGame()
    {
        var seeder = new Random();
        var boardLocations = new List<Location>();

        var playerStart = new Location(
            seeder.Next(_gameOptions.NumberOfRows), 
            0); // Player starts on first column

        boardLocations.Add(playerStart);

        while (boardLocations.Count < _gameOptions.NumberOfMines + 1)
        {
            var location = new Location(
                seeder.Next(_gameOptions.NumberOfRows), 
                seeder.Next(_gameOptions.NumberOfColumns));

            if (!boardLocations.Contains(location)) // Only allow unique mine locations
            {
                boardLocations.Add(location);
            }
        }

        var player = new Player(_gameOptions.MaxLives, playerStart);
        var board = new Board(boardLocations.Skip(1), _gameOptions); // Skip player start location.

        return (board, player);
    }
}