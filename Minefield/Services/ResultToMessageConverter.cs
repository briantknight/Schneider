using MineField.Models;

namespace MineField.Services;

public class ResultToMessageConverter : IConverter<MoveResult, string>
{
    public const string WonMessage = "You have won!";
    public const string LostMessage = "You have lost!";

    public string Convert(MoveResult source) => source switch
    {
        { PlayState: PlayState.Playing } => source.Player.ToString(),
        { PlayState: PlayState.Won } => $"{WonMessage} {source.Player}",
        { PlayState: PlayState.Lost } => $"{LostMessage} {source.Player}",
        _ => throw new NotImplementedException()
    };
}