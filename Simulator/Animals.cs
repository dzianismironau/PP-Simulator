using Simulator;
using Simulator.Maps;

public class Animals : IMappable
{
    public Map? Map { get; private set; }
    public Point Position { get; protected set; }
    public virtual char Symbol => 'A';

    public uint Size { get; set; } = 3;
    private string _description = "Unknown";
    public required string Description

    {
        get { return _description; }
        init
        {
            if (value == null)
            {
                _description = "Unknown";
                return;
            }

            value = value.Trim();
            if (value.Length < 3)
                value = value.PadRight(3, '#');

            if (value.Length > 15)
                value = value.Substring(0, 15).TrimEnd();

            if (char.IsLower(value[0]))
                value = char.ToUpper(value[0]) + value.Substring(1); ;


            _description = value;
        }
    }

    //public string Info
    //{
    //    get
    //    {
    //        return Description + " <" + Size + ">";
    //    }
    //}

    public virtual string Info => $"{Description} <{Size}>"; //np. Dogs <3>

    public virtual void Go(Direction direction)
    {
        if (Map == null) throw new InvalidOperationException("Animal cannot move since it's not on the map!");

        var newPosition = GetNewPosition(direction);

        Map.Remove(this, Position);
        Map.Add(this, newPosition);
        Position = newPosition;
    }

    public virtual void InitMapAndPosition(Map map, Point point)
    {
        if (map == null) throw new ArgumentNullException(nameof(map));
        if (Map != null) throw new InvalidOperationException("This animal is already on a map.");
        if (!map.Exist(point)) throw new ArgumentException("Non-existing position for this map.");
        Map = map;
        Position = point;
        map.Add(this, point);
    }

    protected virtual Point GetNewPosition(Direction direction)
    {
        return Map.Next(Position, direction);
    }
    public override string ToString() => $"{GetType().Name.ToUpper()}: {Info}";

}