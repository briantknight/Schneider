using MineField.Game;
using MineField.Models;

namespace MineField.Tests.Game;

[TestClass]
public class GameControllerTests
{
    private Fixture _fixture;

    [TestInitialize]
    public void TestInitialize()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 20));
    }

    [TestMethod]
    [DataRow(1, 0, Direction.Up, 0, 0, "Can move up")]
    [DataRow(0, 0, Direction.Up, 0, 0, "Cannot exceed upper bound")]
    [DataRow(0, 0, Direction.Down, 1, 0, "Can move down")]
    [DataRow(8, 0, Direction.Down, 8, 0, "cannot exceed lower bound")]
    [DataRow(0, 1, Direction.Right, 0, 2, "Can move Right")]
    [DataRow(0, 1, Direction.Left, 0, 0, "Can move Left")]
    [DataRow(0, 0, Direction.Left, 0, 0, "Cannot exceed left bound")]
    public void ShouldMoveAroundBoard(int startRow, int startColumn, Direction direction, int expectedRow, int expectedColumn, string because)
    {
        // Arrange
        var gameOptions = _fixture.Build<GameOptions>()
            .With(o => o.NumberOfColumns, 8)
            .With(o => o.NumberOfRows, 8)
            .Create();

        var board = new Board(new Location[] { }, gameOptions);
        var player = new Player(100, new Location(startRow, startColumn));
        var expected = new Location(expectedRow, expectedColumn);

        var builder = new Mock<IGameBuilder>();
        builder.Setup(b => b.NewGame()).Returns((board, player));

        var controller = new GameController(builder.Object, gameOptions);

        // Act
        var result = controller.Move(direction);

        // Assert
        result.NewLocation.Should().BeEquivalentTo(expected, because);
    }


    [TestMethod]
    public void ShouldWinGame()
    {
        // Arrange
        var gameOptions = _fixture.Build<GameOptions>()
            .With(o => o.NumberOfColumns, 8)
            .With(o => o.NumberOfRows, 8)
            .Create();

        var board = new Board(Array.Empty<Location>(), gameOptions);
        var player = new Player(100, new Location(0, gameOptions.NumberOfColumns - 1));

        var builder = new Mock<IGameBuilder>();
        builder.Setup(b => b.NewGame()).Returns((board, player));

        var controller = new GameController(builder.Object, gameOptions);

        // Act
        var result = controller.Move(Direction.Right);

        // Assert
        result.PlayState.Should().Be(PlayState.Won);
    }


    [TestMethod]
    [DataRow(1, 0, Direction.Up, 2, 0)]
    [DataRow(1, 0, Direction.Down, 0, 0)]
    [DataRow(0, 1, Direction.Left, 0, 2)]
    [DataRow(0, 1, Direction.Right, 0, 0)]
    public void ShouldLoseGame(int mineRow, int mineColumn, Direction direction, int playerRow, int playerColumn)
    {
        // Arrange
        var gameOptions = _fixture.Build<GameOptions>()
            .With(o => o.NumberOfColumns, 8)
            .With(o => o.NumberOfRows, 8)
            .Create();

        var mineLocation = new Location(mineRow, mineColumn);
        var playerLocation = new Location(playerRow, playerColumn);

        var board = new Board(new[] { mineLocation }, gameOptions);
        var player = new Player(1, playerLocation);

        var builder = new Mock<IGameBuilder>();
        builder.Setup(b => b.NewGame()).Returns((board, player));

        var controller = new GameController(builder.Object, gameOptions);

        // Act
        var result = controller.Move(direction);

        // Assert
        result.PlayState.Should().Be(PlayState.Lost);
    }
}