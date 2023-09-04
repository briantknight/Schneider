using MineField.Models;

namespace MineField.Game;

public static class PlayerMoves
{
    /// <summary>
    /// When a player tries to move off the board
    /// </summary>
    /// <param name="player">The player</param>
    /// <returns>The move with the players current location unchanged</returns>
    public static MoveResult VoidMove(this Player player)
    {
        return new MoveResult(false, player.CurrentLocation, PlayState.Playing, player);
    }

    /// <summary>
    /// Result of a player landing on a empty cell, player moves onto that cell.
    /// </summary>
    /// <param name="player">The player</param>
    /// <param name="newLocation">Location the player moved too</param>
    /// <returns>The result of the move</returns>
    public static MoveResult SuccessMove(this Player player, Location newLocation, Board board)
    {
        player.CurrentLocation = newLocation;
        var gameResult = newLocation.Column == board.NumberOfColumns ? PlayState.Won : PlayState.Playing;
        return new MoveResult(false, newLocation, gameResult, player);
    }

    /// <summary>
    /// Result of a player landing on a mine.
    /// Player looses a life and the game can end.
    /// Player moves back to the start location.
    /// </summary>
    /// <param name="player">The player</param>
    /// <returns>The result of the move</returns>
    public static MoveResult FailedMove(this Player player)
    {
        player.DecrementLives();
        player.CurrentLocation = player.StartLocation;
        var gameResult = player.Lives > 0 ? PlayState.Playing : PlayState.Lost;
        return new MoveResult(true, player.StartLocation, gameResult, player);
    }
}