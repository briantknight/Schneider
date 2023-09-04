using MineField.Models;
using MineField.Services;

namespace MineField.Tests.Services;

[TestClass]
public class KeyToMoveConverterTests
{

    [TestMethod]
    [DataRow('u', Direction.Up)]
    [DataRow('d', Direction.Down)]
    [DataRow('l', Direction.Left)]
    [DataRow('r', Direction.Right)]
    public void ShouldParseValidMoves(char input, Direction expectedResult)
    {
        // Arrange
        var converter = new KeyToMoveParser();

        // Act
        var actual = converter.TryParse(input);

        // Assert
        actual.parsed.Should().BeTrue();
        actual.value.Should().Be(expectedResult);
    }

    [TestMethod]
    public void ShouldFailParseCharToMove()
    {
        // Arrange
        var exclusions = new[] { 'd', 'l', 'r', 'u' };
        var invalidAsciiSet = Enumerable.Range(33, 126)
            .Select(num => (char)num)
            .Where(c => !exclusions.Contains(c));

        var converter = new KeyToMoveParser();

        bool failTest = false;

        // Act
        foreach (var invalidKey in invalidAsciiSet)
        {
            var actual = converter.TryParse(invalidKey);

            if (actual.parsed) // None should parse, this is invalid input!
            {
                failTest = true;
            }
        }

        // Assert
        failTest.Should().BeFalse();
    }
}
