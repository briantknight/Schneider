namespace MineField.Models;

public class Board
{
    private readonly GameOptions _options;

    public Board(IEnumerable<Location> locations, GameOptions options)
    {
        _options = options;
        MineLocations = new List<Location>(locations);
    }

    public IReadOnlyList<Location> MineLocations { get; }

    public int NumberOfColumns => _options.NumberOfColumns;
    public int NumberOfRows => _options.NumberOfRows;
}

