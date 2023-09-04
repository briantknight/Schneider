using MineField.Models;

namespace MineField.Tests.Model;

[TestClass]
public class LocationTests
{
    [TestMethod]
    public void ShouldMoveDown()
    {
        // Arrange
        var row = 33;
        var column = 55;

        var location = new Location(row, column);

        // Act
        var actual = Location.MoveDown(location);

        // Assert
        actual.Row.Should().Be(location.Row + 1);
        actual.Column.Should().Be(location.Column);
    }

    [TestMethod]
    [DataRow(1, 0)]
    [DataRow(0, 0)]
    public void ShouldMoveUpAndKeepInBoundary(int startRow, int expectedRow)
    {
        // Arrange
        var column = 55;

        var location = new Location(startRow, column);

        // Act
        var actual = Location.MoveUp(location);

        // Assert
        actual.Row.Should().Be(expectedRow);
        actual.Column.Should().Be(location.Column);
    }

    [TestMethod]
    public void ShouldMoveRight()
    {
        // Arrange
        var row = 33;
        var column = 55;

        var location = new Location(row, column);

        // Act
        var actual = Location.MoveRight(location);

        // Assert
        actual.Column.Should().Be(location.Column + 1);
        actual.Row.Should().Be(location.Row);
    }

    [TestMethod]
    [DataRow(1, 0)]
    [DataRow(0, 0)]
    public void ShouldMoveLeftAndKeepInBoundary(int startColumn, int expectedColumn)
    {
        // Arrange
        var row = 55;

        var location = new Location(row, startColumn);

        // Act
        var actual = Location.MoveLeft(location);

        // Assert
        actual.Column.Should().Be(expectedColumn);
        actual.Row.Should().Be(location.Row);
    }
}