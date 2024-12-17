﻿using Simulator.Maps;
using Simulator;

public class MapVisualizer
{
    private readonly Map _map;

    public MapVisualizer(Map map)
    {
        _map = map;
    }

    public void Draw()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.Write(Box.TopLeft);
        for (int x = 0; x < _map.SizeX - 1; x++)
        {
            Console.Write($"{Box.Horizontal}{Box.TopMid}");
        }
        Console.WriteLine($"{Box.Horizontal}{Box.TopRight}");

        for (int y = _map.SizeY - 1; y >= 0; y--)
        {
            Console.Write(Box.Vertical);
            for (int x = 0; x < _map.SizeX; x++)
            {
                var creatures = _map.At(x, y);

                if (creatures != null && creatures.Count > 1)
                {
                    Console.Write("X");
                }
                else if (creatures?.Count == 1)
                {
                    var creature = creatures.First();
                    Console.Write(creature.Symbol);
                }
                else
                {
                    Console.Write(" ");
                }

                Console.Write(Box.Vertical);
            }
            Console.WriteLine();

            if (y > 0)
            {
                Console.Write(Box.MidLeft);
                for (int x = 0; x < _map.SizeX - 1; x++)
                {
                    Console.Write($"{Box.Horizontal}{Box.Cross}");
                }
                Console.WriteLine($"{Box.Horizontal}{Box.MidRight}");
            }
        }

        Console.Write(Box.BottomLeft);
        for (int x = 0; x < _map.SizeX - 1; x++)
        {
            Console.Write($"{Box.Horizontal}{Box.BottomMid}");
        }
        Console.WriteLine($"{Box.Horizontal}{Box.BottomRight}");

        Console.WriteLine();
    }
    public void DrawSnapshot(SimulationSnapshot snapshot)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.Write(Box.TopLeft);
        for (int x = 0; x < _map.SizeX - 1; x++)
        {
            Console.Write($"{Box.Horizontal}{Box.TopMid}");
        }
        Console.WriteLine($"{Box.Horizontal}{Box.TopRight}");

        for (int y = _map.SizeY - 1; y >= 0; y--)
        {
            Console.Write(Box.Vertical);
            for (int x = 0; x < _map.SizeX; x++)
            {
                var point = new Point(x, y);

                if (snapshot.MapState.ContainsKey(point))
                {
                    var creatures = snapshot.MapState[point];
                    if (creatures.Count > 1)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        var creature = creatures.First();
                        Console.Write(creature.Symbol);
                    }
                }
                else
                {
                    Console.Write(" ");
                }

                Console.Write(Box.Vertical);
            }
            Console.WriteLine();

            if (y > 0)
            {
                Console.Write(Box.MidLeft);
                for (int x = 0; x < _map.SizeX - 1; x++)
                {
                    Console.Write($"{Box.Horizontal}{Box.Cross}");
                }
                Console.WriteLine($"{Box.Horizontal}{Box.MidRight}");
            }
        }

        Console.Write(Box.BottomLeft);
        for (int x = 0; x < _map.SizeX - 1; x++)
        {
            Console.Write($"{Box.Horizontal}{Box.BottomMid}");
        }
        Console.WriteLine($"{Box.Horizontal}{Box.BottomRight}");

        Console.WriteLine();
    }

}