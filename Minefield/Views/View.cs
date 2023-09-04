using MineField.Game;
using MineField.Models;
using MineField.Services;

namespace MineField.Views;

public class View : IView
{
    private readonly IGameController _controller;
    private readonly IHmi _hmi;

    public View(IGameController controller, IHmi hmi)
    {
        _controller = controller;
        _hmi = hmi;
    }

    public void Play()
    {
        _hmi.ReportStartMessage();

        PlayState playState;
        do
        {
            var moveDirection = _hmi.GetNextMove();
            var latestMoveResult = _controller.Move(moveDirection);
            playState = latestMoveResult.PlayState;
            _hmi.ReportLatest(latestMoveResult);
        } while (playState == PlayState.Playing);
    }
}