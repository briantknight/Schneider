namespace MineField.Models;

/// <summary>
/// The player of the game
/// </summary>
public class Player
{
    private int _lives;
    private int _moves;

    public Player(int maxLives, Location startLocation)
    {
        _lives = maxLives;
        StartLocation = startLocation;
        CurrentLocation = StartLocation;
    }

    /// <summary>
    /// Location randomly set on the first column
    /// </summary>
    public Location StartLocation { get; }

    /// <summary>
    /// Players current location
    /// </summary>
    public Location CurrentLocation { get; set; }

    /// <summary>
    /// Number of lives
    /// </summary>
    public int Lives => _lives;

    /// <summary>
    /// Move count
    /// </summary>
    public int Moves => _moves;

    /// <summary>
    /// Player lost a life
    /// </summary>
    public void DecrementLives() => _lives--;

    /// <summary>
    /// Player made a move
    /// </summary>
    public void IncrementMoves() => _moves++;

    public override string ToString()
    {
        return $"Position : {CurrentLocation}, Lives left: {Lives}, Attempts : {Moves}";
    }
}