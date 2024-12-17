namespace Simulator.Maps;

public abstract class BigMap : Map
{
    private Dictionary<Point, List<IMappable>> _fields;

    public BigMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 1000 || sizeY > 1000)
        {
            throw new ArgumentOutOfRangeException("Map size is too large.");
        }

        _fields = new Dictionary<Point, List<IMappable>>();
    }

    public override void Add(IMappable mappable, Point position)
    {
        if (!Exist(position))
            throw new ArgumentException("Pozycja poza mapą.");

        if (!_fields.ContainsKey(position))
        {
            _fields[position] = new List<IMappable>();
        }

        _fields[position].Add(mappable);
    }

    public override void Remove(IMappable mappable, Point position)
    {
        if (!Exist(position))
            throw new ArgumentException("Pozycja poza mapą.");

        if (_fields.ContainsKey(position))
        {
            _fields[position].Remove(mappable);
            if (_fields[position].Count == 0)
            {
                _fields.Remove(position);
            }
        }
    }

    public override List<IMappable>? At(Point position)
    {
        if (!Exist(position))
            throw new ArgumentException("Pozycja poza mapą.");

        return _fields.ContainsKey(position) ? _fields[position] : null;
    }

    public override List<IMappable>? At(int x, int y) => At(new Point(x, y));
}