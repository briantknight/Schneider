using MineField.Models;
using MineField.Services;

namespace MineField.Tests.Services;

[TestClass]
public class ResultToMessageConverterTests
{

    [TestMethod]
    [DataRow(PlayState.Won, ResultToMessageConverter.WonMessage)]
    [DataRow(PlayState.Lost, ResultToMessageConverter.LostMessage)]
    [DataRow(PlayState.Playing, "")]
    public void ShouldConvertResultToMessage(PlayState playState, string messageStartsWith)
    {
        // Arrange
        var result = new MoveResult(false, new Location(1, 1), playState, new Player(3, new Location(1, 1)));

        var converter = new ResultToMessageConverter();

        // Act
        var actual = converter.Convert(result);

        // Assert
        actual.Should().StartWith(messageStartsWith);
        actual.Should().EndWith(result.Player.ToString());
    }
}