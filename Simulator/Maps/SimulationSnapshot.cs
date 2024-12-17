using Simulator.Maps;

namespace Simulator;

public class SimulationSnapshot
{
    public Dictionary<Point, List<IMappable>> MapState { get; }

    public int TurnNumber { get; }

    public SimulationSnapshot(Simulation simulation)
    {
        TurnNumber = simulation._index + 1;
        MapState = new Dictionary<Point, List<IMappable>>();

        for (int x = 0; x < simulation.Map.SizeX; x++)
        {
            for (int y = 0; y < simulation.Map.SizeY; y++)
            {
                var creatures = simulation.Map.At(x, y);

                if (creatures != null && creatures.Count > 0)
                {
                    MapState[new Point(x, y)] = new List<IMappable>(creatures);
                }
            }
        }
    }
}