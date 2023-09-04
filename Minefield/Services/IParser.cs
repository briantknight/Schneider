namespace MineField.Services;

public interface IParser<Tin, Tout>
{
    (bool parsed, Tout value) TryParse(Tin source);
}