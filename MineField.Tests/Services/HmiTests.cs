using MineField.Models;
using MineField.Services;

namespace MineField.Tests.Services;

[TestClass]
public class HmiTests
{

    Fixture _fixture;

    [TestInitialize]
    public void TestInitialize()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new RandomNumericSequenceGenerator(1, 20));
    }
    
    [TestMethod]
    [Timeout(1000)]
    [DataRow('u', Direction.Up)]
    [DataRow('d', Direction.Down)]
    [DataRow('l', Direction.Left)]
    [DataRow('r', Direction.Right)]
    public void ShouldReadAndParseUserInput(char input, Direction expectedDirection)
    {
        // Arrange
        var userInput = 'u';

        var userIo = new Mock<IUserIo>();
        userIo.Setup(uio => uio.GetInput()).Returns(userInput);

        var parser = new Mock<IParser<char, Direction>>();
        parser.Setup(p => p.TryParse(userInput)).Returns((true, expectedDirection));

        var hmi = new Hmi(
            userIo.Object, 
            parser.Object,
            Mock.Of<IConverter<MoveResult, string>>());

        // Act
        var actualDirection = hmi.GetNextMove();

        // Assert
        userIo.Verify(uio => uio.GetInput());
        parser.Verify(p => p.TryParse(userInput));
        actualDirection.Should().Be(expectedDirection);
    }
    
    [TestMethod]
    [Timeout(1000)]
    public void ShouldContinueReadingUntilValidKeypress()
    {
        var userIo = new Mock<IUserIo>();
        userIo.Setup(uio => uio.GetInput()).Returns('u');

        var parser = new Mock<IParser<char, Direction>>();
        parser.SetupSequence(p => p.TryParse(It.IsAny<char>()))
            .Returns((false, Direction.Right))
            .Returns((false, Direction.Right))
            .Returns((false, Direction.Right))
            .Returns((false, Direction.Right))
            .Returns((false, Direction.Right))
            .Returns((false, Direction.Right))
            .Returns((true, Direction.Up));

        var hmi = new Hmi(
            userIo.Object,
            parser.Object,
            Mock.Of<IConverter<MoveResult, string>>());

        // Act
        var actualDirection = hmi.GetNextMove();

        // Assert
        actualDirection.Should().Be(Direction.Up);
    }

    [TestMethod]
    [Timeout(1000)]
    public void ShouldWriteMessages()
    {
        // Arrange
        var expectedMessage = _fixture.Create<string>();

        var moveResult = _fixture.Create<MoveResult>();

        var userIo = new Mock<IUserIo>();
        userIo.Setup(uio => uio.GetInput()).Returns('u');

        var converter = new Mock<IConverter<MoveResult, string>>();
        converter.Setup(c => c.Convert(moveResult))
            .Returns(expectedMessage);

        var hmi = new Hmi(
            userIo.Object,
            Mock.Of<IParser<char, Direction>>(),
            converter.Object
        );

        // Act
        hmi.ReportLatest(moveResult);

        // Assert
        userIo.Verify(uio => uio.WriteMessage(expectedMessage));
    }
}