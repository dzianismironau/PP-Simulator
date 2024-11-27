using Simulator.Maps;

namespace Simulator;

public class Simulation
{
    /// <summary>
    /// Simulation's map.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// IMappables moving on the map.
    /// </summary>
    public List<IMappable> Mappables { get; }

    /// <summary>
    /// Starting positions of mappables.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Cyclic list of mappables moves. 
    /// Bad moves are ignored - use DirectionParser.
    /// First move is for first mappable, second for second and so on.
    /// When all mappables make moves, 
    /// next move is again for first mappable and so on.
    /// </summary>
    public string Moves { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    public bool Finished = false;

    /// <summary>
    /// Current turn counter.
    /// </summary>
    private int _counter = 0;

    /// <summary>
    /// Valid moves chars.
    /// </summary>
    private HashSet<char> validMoves = new HashSet<char> { 'l', 'r', 'u', 'd' };

    /// <summary>
    /// IMappable which will be moving current turn.
    /// </summary>
    public IMappable CurrentMappable
    {
        get => Mappables[_counter % Mappables.Count];
    }

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary>
    public string CurrentMoveName
    {
        get
        {
            var direction = DirectionParser.Parse(Moves[_counter % Moves.Length].ToString());
            if (direction.Any())
            {
                return direction[0].ToString().ToLower();
            }
            return string.Empty;
        }
    }

    /// <summary>
    /// Simulation constructor.
    /// Throw errors:
    /// if mappables' list is empty,
    /// if number of mappables differs from 
    /// number of starting positions.
    /// </summary>
    public Simulation(Map map, List<IMappable> mappables,
        List<Point> positions, string moves)
    {
        if (mappables == null || mappables.Count == 0)
        {
            throw new ArgumentException("IMappables list cannot be empty.");
        }
        if (positions == null || positions.Count != mappables.Count)
        {
            throw new ArgumentException("Positions count does not match the number of mappables.");
        }
        if (string.IsNullOrWhiteSpace(moves))
        {
            throw new ArgumentException("Moves string cannot be empty or null.");
        }
        Map = map ?? throw new ArgumentNullException(nameof(map));
        Mappables = mappables;
        Positions = positions;
        Moves = ValidateMoves(moves);
        for (int i = 0; i < mappables.Count; i++)
        {
            mappables[i].InitMapAndPosition(map, positions[i]);
        }
    }

    /// <summary>
    /// Makes one move of current mappable in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn()
    {
        if (Finished)
        {
            throw new InvalidOperationException("Simulation is already finished.");
        }
        var direction = DirectionParser.Parse(Moves[_counter % Moves.Length].ToString())[0];
        CurrentMappable.Go(direction);
        _counter++;
        if (_counter >= Moves.Length) Finished = true;
    }

    /// <summary>
    /// Validates moves input.
    /// </summary>
    private string ValidateMoves(string moves) => new (moves.Where(c => validMoves.Contains(Char.ToLower(c))).ToArray());
}