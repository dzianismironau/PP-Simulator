namespace Simulator;

public class Elf : Creature
{
    private int _agility;
    private int _singCount;
    public override char Symbol => 'E';
    public int Agility
    {
        get { return _agility; }
        private set
        {
            if (value < 0) _agility = 0;
            else if (value > 10) _agility = 10;
            else _agility = value;
        }
    }

    public void Sing()
    {
    }

    public override int Power => 8 * Level + 2 * Agility;

    public Elf() : base() { }
    public Elf(string name, int level = 1, int agility = 1) : base(name, level)
    {
        Agility = agility;
    }

    public string Greeting { get; }

    public override string Info
    {
        get { return $"{Name} [{Level}][{Agility}]"; }
    }
}