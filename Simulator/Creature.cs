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
        get { return _name; }
        init
        {
            if (value == null)
            {
                _name = "Unknown";
                return;
            }

            value = value.Trim(); // Usuń nadmiarowe spacje na początku i końcu

            if (value.Length < 3)
                value = value.PadRight(3, '#');

            if (value.Length > 25)
                value = value.Substring(0, 25).TrimEnd();

            if (char.IsLower(value[0]))
                value = char.ToUpper(value[0]) + value.Substring(1); ;

            _name = value;
        }
    }

    public int Level
    {
        get { return _level; }
        init
        {
            if (value < 1) _level = 1;
            else if (value > 10) _level = 10;
            else _level = value;

        }
    }

    public Creature(string name, int level = 1)
    {
        Name = name; Level = level;
    }

    public Creature() { }

    public string Greeting { get; }
    public abstract int Power { get; }

    public abstract string Info { get; }
    public virtual char Symbol => 'C';

    public void Upgrade()
    {
        if (Level < 10)
            _level++;
    }

    public void InitMapAndPosition(Map map, Point position)
    {
        if (map == null) throw new ArgumentNullException(nameof(map));
        if (Map != null) throw new InvalidOperationException($"Ten stwor już znajduje się na mapie. Nie można go przenieść na nową mapę.");
        if (!map.Exist(position)) throw new ArgumentException("Tego miejsca nie ma na mapie.");

        Map = map;
        Position = position;
        Map.Add(this, position);
    }

    public void Go(Direction direction)
    {
        if (Map == null) throw new InvalidOperationException("Ten stwor nie może się ruszyć, ponieważ nie jest na mapie.");
        var newPosition = Map.Next(Position, direction);
        Map.Move(this, Position, newPosition);
        Position = newPosition;

    }

    public override string ToString() => $"{GetType().Name.ToUpper()}: {Info}";
}