namespace Simulator;

public class SimulationHistory
{
    private readonly List<SimulationSnapshot> _snapshots = new List<SimulationSnapshot>();
    private readonly Simulation _simulation;

    public int TotalTurns => _snapshots.Count;

    public SimulationHistory(Simulation simulation)
    {
        _simulation = simulation;
        RunSimulationAndRecordSnapshots();
    }

    private void RunSimulationAndRecordSnapshots()
    {
        while (!_simulation.Finished)
        {
            _snapshots.Add(new SimulationSnapshot(_simulation));
            _simulation.Turn();
        }
    }

    public SimulationSnapshot GetSnapshot(int turn)
    {
        return _snapshots[turn];
    }

    public void TakeSnapshot(Simulation simulation)
    {
        _snapshots.Add(new SimulationSnapshot(simulation));
    }
}