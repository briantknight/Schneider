using MineField.Game;
using MineField.Models;

namespace MineField.Tests.Game;

[TestClass]
public class GameBuilderTests
{
    private Fixture _fixture;

    [TestInitialize]
    public void TestInitialize()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 20));
    }

    [TestMethod]
    public void ShouldCreatePlayerWithCorrectNumberOfLives()
    {
        // Arrange
        var options = _fixture.Create<GameOptions>();

        var builder = new GameBuilder(options);

        // Act
        var game = builder.NewGame();

        // Assert
        game.player.Lives.Should().Be(options.MaxLives);
    }


    [TestMethod]
    public void ShouldStartPlayerWithStartPositionAnywhereAlongFirstColumn()
    {
        // Arrange
        var options = _fixture.Create<GameOptions>();

        var builder = new GameBuilder(options);

        // Act
        var game = builder.NewGame();

        // Assert
        game.player.StartLocation.Column.Should().Be(0);
        game.player.CurrentLocation.Row.Should().BeInRange(0, options.NumberOfRows);
        game.player.CurrentLocation.Should().Be(game.player.StartLocation);
    }

    [TestMethod]
    public void ShouldCreateBoardWithExpectedNumberOfRandomMineLocations()
    {
        // Arrange
        var options = _fixture.Create<GameOptions>();

        var builder = new GameBuilder(options);

        // Act
        var game = builder.NewGame();

        // Assert
        game.board.MineLocations.Should().HaveCount(options.NumberOfMines);
        game.board.MineLocations.Should().OnlyHaveUniqueItems();
    }

    [TestMethod]
    public void ShouldCreateBoardWithExpectedSize()
    {
        // Arrange
        var options = _fixture.Create<GameOptions>();

        var builder = new GameBuilder(options);

        // Act
        var game = builder.NewGame();

        // Assert
        game.board.NumberOfRows.Should().Be(options.NumberOfRows);
        game.board.NumberOfColumns.Should().Be(options.NumberOfColumns);
    }
}