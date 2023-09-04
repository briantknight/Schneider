using MineField.Models;

namespace MineField.Services;

public interface IHmi
{
    Direction GetNextMove();
    void ReportLatest(MoveResult moveResult);

    void ReportStartMessage();
}