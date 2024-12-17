namespace Simulator.Maps;

public class BigTorusMap : BigMap
{
    public BigTorusMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
    }

    public override List<IMappable>? At(int x, int y)
    {
        throw new NotImplementedException();
    }

    public override Point Next(Point p, Direction d)
    {
        var moved = p.Next(d);
        return new Point((moved.X + SizeX) % SizeX, (moved.Y + SizeY) % SizeY);
    }
    public override Point NextDiagonal(Point p, Direction d)
    {
        var moved = p.NextDiagonal(d);
        return new Point((moved.X + SizeX) % SizeX, (moved.Y + SizeY) % SizeY);
    }
}