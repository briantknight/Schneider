namespace MineField.Services;

public interface IUserIo
{
    public char GetInput();

    public void WriteMessage(string message);
}