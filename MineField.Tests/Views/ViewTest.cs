using MineField.Game;
using MineField.Models;
using MineField.Services;
using MineField.Views;

namespace MineField.Tests.Views;

[TestClass]
public class ViewTest
{

    [TestMethod]
    [Timeout(1000)]
    [DataRow(PlayState.Won)]
    [DataRow(PlayState.Lost)]
    public void ShouldEndGame(PlayState playState)
    {
        // Arrange
        var moveResult = new MoveResult(false, new Location(0, 0),
            playState, new Player(1, new Location(0, 0)));

        var hmi = new Mock<IHmi>();

        hmi.Setup(h => h.GetNextMove()).Returns(Direction.Right);

        var controller = new Mock<IGameController>();
        controller.Setup(c => c.Move(It.IsAny<Direction>())).Returns(moveResult);


        var view = new View(controller.Object, hmi.Object);

        // Act
        view.Play();

        // Assert
        hmi.Verify(h => h.ReportLatest(It.Is<MoveResult>(mr => mr.PlayState == playState)));
    }


    [TestMethod]
    [Timeout(1000)]
    [DataRow(PlayState.Won)]
    [DataRow(PlayState.Lost)]
    public void ShouldPlayGameToEnd(PlayState endGamePlayState)
    {
        // Arrange
        var stillPlayingResult = new MoveResult(false, new Location(0, 0),
            PlayState.Playing, new Player(1, new Location(0, 0)));

        var endGameResult = new MoveResult(false, new Location(0, 0),
            endGamePlayState, new Player(1, new Location(0, 0)));

        var hmi = new Mock<IHmi>();

        hmi.Setup(h => h.GetNextMove()).Returns(Direction.Right);

        var controller = new Mock<IGameController>();
        controller.SetupSequence(c => c.Move(Direction.Right))
            .Returns(stillPlayingResult)
            .Returns(stillPlayingResult)
            .Returns(stillPlayingResult)
            .Returns(stillPlayingResult)
            .Returns(stillPlayingResult)
            .Returns(endGameResult);

        var view = new View(controller.Object, hmi.Object);

        // Act
        view.Play();

        // Assert
        controller.Verify(c => c.Move(Direction.Right), Times.Exactly(6));
    }
}