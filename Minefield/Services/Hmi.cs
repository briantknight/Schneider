using MineField.Models;

namespace MineField.Services;

public class Hmi : IHmi
{
    private readonly IUserIo _userIo;
    private readonly IParser<char, Direction> _keyToMoveParser;
    private readonly IConverter<MoveResult, string> _resultToMessageConverter;

    public Hmi(IUserIo userIo, IParser<char, Direction> keyToMoveParser, IConverter<MoveResult, string> resultToMessageConverter)
    {
        _userIo = userIo;
        _keyToMoveParser = keyToMoveParser;
        _resultToMessageConverter = resultToMessageConverter;
    }

    public Direction GetNextMove()
    {
        bool  validKeypress;
        Direction moveDirection;

        do
        {
            var keyPress = _userIo.GetInput();

            var input = _keyToMoveParser.TryParse(keyPress);

            validKeypress = input.parsed;
            moveDirection = input.value;

        } while (!validKeypress);

        return moveDirection;
    }

    public void ReportLatest(MoveResult moveResult)
    {
        var message = _resultToMessageConverter.Convert(moveResult);
        _userIo.WriteMessage(message);
    }

    public void ReportStartMessage()
    {
        _userIo.WriteMessage("press l for left, r for right, u for up, d for down. Good luck!");
    }
}