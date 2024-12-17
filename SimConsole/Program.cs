using Simulator;
using Simulator.Maps;

namespace SimConsole;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;


        Map map = new BigBounceMap(8, 6);

        List<IMappable> creatures = new List<IMappable>
        {
            new Elf("Elandor"),
            new Orc("Gorbag"),
            new Animals { Description = "Rabbit", Size = 3 },
            new Birds { Description = "Eagle", Size = 2, CanFly = true  },
            new Birds { Description = "Ostrich", Size = 5, CanFly = false },
        };

        List<Point> positions = new List<Point>
        {
            new Point(2, 2),
            new Point(3, 1),
            new Point(4, 1),
            new Point(1, 3),
            new Point(6, 4),
        };

        string moves = "udlrudlrudlrudlrudlr";

        try
        {
            var simulation = new Simulation(map, creatures, positions, moves);

            var simulationHistory = new SimulationHistory(simulation);

            while (!simulation.Finished)
            {
                Console.Clear();
                Console.WriteLine("SIMULATION!");
                Console.WriteLine($"Turn {simulation._index + 1}");

                simulation.Turn();

                var mapVisualizer = new MapVisualizer(map);
                mapVisualizer.Draw();

                System.Threading.Thread.Sleep(500);
            }

            Console.WriteLine("Simulation finished!");
            Console.WriteLine("\n=== Simulation History ===");
            int[] turnsToShow = { 5, 10, 15, 20 };

            foreach (var turn in turnsToShow)
            {
                if (turn - 1 < simulationHistory.TotalTurns)
                {
                    Console.WriteLine($"\nTurn {turn}:");
                    var snapshot = simulationHistory.GetSnapshot(turn - 1);

                    var mapVisualizer = new MapVisualizer(map);
                    mapVisualizer.DrawSnapshot(snapshot);
                }
                else
                {
                    Console.WriteLine($"\nTurn {turn}: Not available (simulation finished earlier)");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}