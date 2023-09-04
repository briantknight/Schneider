namespace MineField.Services;

public interface IConverter<Tin, Tout>
{
    Tout Convert(Tin source);
}