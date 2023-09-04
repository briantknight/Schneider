using MineField.Models;

namespace MineField.Services;

public class KeyToMoveParser : IParser<char, Direction>
{
    public (bool parsed, Direction value) TryParse(char source) => source switch
    {
        'u' => (true, Direction.Up),
        'l' => (true, Direction.Left),
        'r' => (true, Direction.Right),
        'd' => (true, Direction.Down),
        _ => (false, Direction.Down)
    };
}