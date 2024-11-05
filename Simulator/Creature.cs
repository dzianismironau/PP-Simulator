﻿namespace Simulator;
public abstract class Creature
{
    private string _name = "Unknown";
    private int _level = 1;
    public string Name
    {
        get => _name; set
        {
            if (_name != "Unknown") return;
            _name = value?.Trim() ?? "Unknown";

            if (_name.Length < 3)
                _name = _name.PadRight(3, '#');

            if (_name.Length > 25)
            {
                _name = _name.Substring(0, 25).TrimEnd();
                if (_name.Length < 3) _name = _name.PadRight(3, '#');
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
            if (value < 1) _level = 1;
            else if (value > 10) _level = 10;
            else _level = value;
        }
    }
    public Creature(string name, int level = 1)
    {
        Name = name; Level = level;
    }
    public Creature()
    { }
    public abstract void SayHi();
    public abstract int Power { get; }
    public abstract string Info { get; }
    public void Upgrade()
    {
        if (Level < 10) Level++;
    }
    public void Go(Direction direction)
    {
        Console.WriteLine($"{Name} goes {direction.ToString().ToLower()}");
    }

    public void Go(Direction[] directions)
    {
        for (int i = 0; i < directions.Length; i++)
        {
            Go(directions[i]);
        }
    }
    public void Go(string directions)
    {
        Go(DirectionParser.Parse(directions));
    }
    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}

