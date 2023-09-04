using System.Diagnostics.CodeAnalysis;

namespace MineField.Services;

[ExcludeFromCodeCoverage]
public class UserIo : IUserIo
{
    public char GetInput()
    {
        return (char)Console.Read();
    }

    public void WriteMessage(string message)
    {
        Console.WriteLine(message);
    }
}

