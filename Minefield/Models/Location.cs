using System.Drawing;

namespace MineField.Models;

public class Location
{
    public int Row { get; }
    public int Column { get; }

    public Location(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public static Location MoveUp(Location source) => new (source.Row > 0 ? source.Row -1 : 0, source.Column);
    public static Location MoveDown(Location source) => new (source.Row + 1, source.Column);
    public static Location MoveLeft(Location source) => new (source.Row, source.Column > 0 ? source.Column - 1 : 0);
    public static Location MoveRight(Location source) => new (source.Row, source.Column + 1);


    public static bool operator ==(Location a, Location b)
    {
        return a.Column == b.Column && a.Row == b.Row;
    }

    public static bool operator !=(Location a, Location b)
    {
        return !(a == b);
    }

    public override string ToString()
    {
        return $"{ColumnCharacter}{Row}";
    }

    private char ColumnCharacter => Convert.ToChar(65 + Column);

    public override bool Equals(object? obj)
    {
        return obj is Location location && this == location;
    }

    public override int GetHashCode() => HashCode.Combine(Column, Row);

}
