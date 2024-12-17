using Simulator.Maps;

namespace Simulator
{
    public class Simulation
    {
        /// <summary>
        /// Simulation's map.
        /// </summary>
        public Map Map { get; }

        /// <summary>
        /// Creatures moving on the map.
        /// </summary>
        public List<IMappable> Mappables { get; }

        /// <summary>
        /// Starting positions of creatures.
        /// </summary>
        public List<Point> Positions { get; }

        /// <summary>
        /// Cyclic list of creatures moves. 
        /// Bad moves are ignored - use DirectionParser.
        /// First move is for first creature, second for second and so on.
        /// When all creatures make moves, 
        /// next move is again for first creature and so on.
        /// </summary>
        public string Moves { get; }

        /// <summary>
        /// Has all moves been done?
        /// </summary>
        public bool Finished = false;

        public int _index = 0;

        /// <summary>
        /// Creature which will be moving current turn.
        /// </summary>
        public IMappable CurrentMappable
        {
            get => Mappables[_index % Mappables.Count];
        }

        /// <summary>
        /// Lowercase name of direction which will be used in current turn.
        /// </summary>
        public string CurrentMoveName
        {
            get => Moves[_index % Moves.Length].ToString().ToLower();
        }

        /// <summary>
        /// Simulation constructor.
        /// Throws errors if:
        /// - creatures' list is empty
        /// - number of creatures differs from number of starting positions
        /// - moves string is null or empty
        /// </summary>
        public Simulation(Map map, List<IMappable> mappables,
            List<Point> positions, string moves)
        {
            if (mappables == null || mappables.Count == 0)
            {
                throw new ArgumentException("Creature list cannot be empty.");
            }

            if (positions == null || positions.Count != mappables.Count)
            {
                throw new ArgumentException("The number of initial positions does not match the number of creatures.");
            }

            if (string.IsNullOrEmpty(moves))
            {
                throw new ArgumentException("Moves string cannot be empty or null.");
            }

            Map = map ?? throw new ArgumentNullException(nameof(map));
            Mappables = mappables;
            Positions = positions;
            Moves = moves;

            for (int i = 0; i < Mappables.Count; i++)
            {
                mappables[i].InitMapAndPosition(map, positions[i]);
            }
        }

        /// <summary>
        /// Makes one move of current creature in current direction.
        /// Throws an error if simulation is finished.
        /// </summary>
        public void Turn()
        {
            if (Finished)
            {
                throw new InvalidOperationException("Simulation is already finished.");
            }

            char moveChar = Moves[_index % Moves.Length];

            Direction direction;
            try
            {
                direction = DirectionParser.Parse(moveChar.ToString())[0];
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Invalid move character: '{moveChar}'. Valid moves: 'U', 'D', 'L', 'R'.");
            }


            CurrentMappable.Go(direction);
            _index++;

            if (_index >= Moves.Length)
            {
                Finished = true;
            }
        }
    }
}
