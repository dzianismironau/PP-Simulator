﻿namespace Simulator.Maps;

public interface IMappable
{
    public char Symbol { get; }
    void Go(Direction direction);
    void InitMapAndPosition(Map map, Point point);
    public string ToString();
}