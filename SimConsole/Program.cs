using Simulator;
using Simulator.Maps;
using System.Collections.Generic;

namespace SimConsole
{
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

            var simulation = new Simulation(map, creatures, positions, moves);

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
        }
    }
}