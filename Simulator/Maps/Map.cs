using System.Drawing;
namespace Simulator.Maps;

public abstract class Map
{
    private readonly Rectangle _map;
    public readonly int SizeX;
    public readonly int SizeY;
    protected Map(int sizeX, int sizeY)
    {
        if (sizeX < 5)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Too narrow.");
        }
        if (sizeY < 5)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeY), "Too short.");
        }
        SizeX = sizeX;
        SizeY = sizeY;
        _map = new Rectangle(0, 0, SizeX - 1, SizeY - 1);
    }

    public virtual bool Exist(Point p) => _map.Contains(p);

    public abstract Point Next(Point p, Direction d);

    public abstract Point NextDiagonal(Point p, Direction d);
}