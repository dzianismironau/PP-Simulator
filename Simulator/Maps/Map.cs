namespace Simulator.Maps;

public abstract class Map
{
    private readonly Rectangle _bounds;

    public int SizeX { get; }
    public int SizeY { get; }

    protected Dictionary<Point, List<IMappable>> _fields;

    protected Map(int sizeX, int sizeY)
    {
        if (sizeX < 5)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Too narrow");
        }
        if (sizeY < 5)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Too short");
        }
        SizeX = sizeX;
        SizeY = sizeY;

        _bounds = new Rectangle(0, 0, SizeX - 1, SizeY - 1);
        _fields = new Dictionary<Point, List<IMappable>>();
    }

    public void Add(IMappable mappable, Point position)
    {
        if (!Exist(position))
            throw new ArgumentException("Position is out of map bounds.");

        if (!_fields.ContainsKey(position))
        {
            _fields[position] = new List<IMappable>();
        }

        _fields[position].Add(mappable);
    }
    public void Remove(IMappable mappable, Point position)
    {
        if (!Exist(position))
            throw new ArgumentException("Position is out of map bounds.");

        if (_fields.ContainsKey(position))
        {
            List<IMappable> entitiesAtPosition = _fields[position];

            if (entitiesAtPosition.Contains(mappable))
            {
                entitiesAtPosition.Remove(mappable);
                if (entitiesAtPosition.Count == 0)
                {
                    _fields.Remove(position);
                }
            }
            else
            {
                throw new ArgumentException("The mappable entity is not at the specified position.");
            }
        }
        else
        {
            throw new ArgumentException("No entities at the specified position.");
        }
    }
    public void Move(IMappable mappable, Point positionFrom, Point positionTo)
    {
        if (!Exist(positionFrom) || !Exist(positionTo)) throw new ArgumentException("Jedna z pozycji jest poza mapą!");
        Remove(mappable, positionFrom);
        Add(mappable, positionTo);
    }

    public List<IMappable>? At(Point position)
    {
        if (!Exist(position))
            throw new ArgumentException("Position is out of map bounds.");
        return _fields.ContainsKey(position) ? _fields[position] : null;
    }

    public List<IMappable>? At(int x, int y) => At(new Point(x, y));
    /// <summary>
    /// Check if give point belongs to the map.
    /// </summary>
    /// <param name="p">Point to check.</param>
    /// <returns></returns>
    public virtual bool Exist(Point p) => _bounds.Contains(p);

    /// <summary>
    /// Next position to the point in a given direction.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point Next(Point p, Direction d);

    /// <summary>
    /// Next diagonal position to the point in a given direction 
    /// rotated 45 degrees clockwise.
    /// </summary>
    /// <param name="p">Starting point.</param>
    /// <param name="d">Direction.</param>
    /// <returns>Next point.</returns>
    public abstract Point NextDiagonal(Point p, Direction d);
}