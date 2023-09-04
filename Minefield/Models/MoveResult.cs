namespace MineField.Models;

public class MoveResult
{
    public MoveResult(bool lifeLost, Location newLocation, PlayState playState, Player player)
    {
        LifeLost = lifeLost;
        NewLocation = newLocation;
        PlayState = playState;
        Player = player;
    }

    public bool LifeLost { get; }
    public Location NewLocation { get; }
    public PlayState PlayState { get; }
    public Player Player { get; }
}