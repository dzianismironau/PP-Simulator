using Simulator.Maps;

namespace Simulator;
public abstract class Creature : IMappable
{
    public Map? Map { get; private set; }
    public Point Position { get; private set; }

    private string _name = "Unknown";

    private int _level = 1;

    public string Name
    {
        get => _name;
        set
        {   
            if (_name != "Unknown") return;

            _name = value?.Trim() ?? "Unknown";

            if (_name.Length < 3)
                _name = _name.PadRight(3, '#');

            if (_name.Length > 25)
            {
                _name = _name.Substring(0, 25).TrimEnd();

                if (_name.Length < 3)
                    _name = _name.PadRight(3, '#');
            }

            if (char.IsLower(_name[0]))
                _name = char.ToUpper(_name[0]) + _name.Substring(1);
        }
    }
    public int Level
    {
        get => _level;
        set
        {
            if (value < 1)
                _level = 1;
            else if (value > 10)
                _level = 10;
            else _level = value;

        }
    }

    public Creature(string name, int level = 1)
    {
        Name = name;
        Level = level;
    }

    public Creature()
    { }

    public abstract string Greeting();

    public abstract int Power { get; }

    public abstract string Info
    {
        get;
    }

    public void Upgrade()
    {
        if (Level < 10)
            Level++;
    }

    public void InitMapAndPosition(Map map, Point position)
    {
        if (map == null)
        {
            throw new ArgumentNullException(nameof(map));
        }
        if (Map != null)
        {
            throw new InvalidOperationException($"This creature is already on a map. It cannot be moved to a new map.");
        }
        if (!map.Exist(position))
        {
            throw new ArgumentException("Non-existing position for this map.");
        }
        Map = map;
        Position = position;
        map.Add(this, position);
    }

    public string Go(Direction direction)
    {
        if (Map == null)
        {
            throw new InvalidOperationException("Creature cannot move since it's not on the map!");
        }
        var newPosition = Map.Next(Position, direction);
        Map.Move(this, Position, newPosition);
        Position = newPosition;
        return $"{Name} goes {direction.ToString().ToLower()}.";
    }

    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}