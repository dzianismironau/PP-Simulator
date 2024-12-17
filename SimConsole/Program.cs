using Simulator;
using Simulator.Maps;

namespace SimConsole;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;


        BigBounceMap map = new BigBounceMap(8, 6);

        List<IMappable> creatures = new()
        {
            new Elf("Elandor"),
            new Orc("Gorbag"),
            new Animals { Description = "Rabbit", Size = 3 },
            new Birds { Description = "Eagle", Size = 2, CanFly = true  },
            new Birds { Description = "Ostrich", Size = 5, CanFly = false },
        };

        List<Point> positions = new()
        {
            new Point(2, 2),
            new Point(3, 1),
            new Point(4, 1),
            new Point(1, 3),
            new Point(6, 4),
        };

        string moves = "udlrudlrudlrudlrudlr";


        Simulation simulation = new Simulation(map, creatures, positions, moves);
        MapVisualizer mapVisualizer = new MapVisualizer(map);

        Console.WriteLine("SIMULATION!");
        Console.WriteLine();
        Console.WriteLine("Starting positions:");

        mapVisualizer.Draw();
        var turn = 0;

        var history = new SimulationHistory(simulation);
        var logVisualizer = new LogVisualizer(history);

        logVisualizer.Draw(5);
        logVisualizer.Draw(10);
        logVisualizer.Draw(15);
        logVisualizer.Draw(20);


    }
}