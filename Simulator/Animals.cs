namespace Simulator;

    public class Animals
    {
    private string _description = "Unknown";
    public string Description 
    { 
        get=> _description;
        set
        {
            _description = value?.Trim() ?? "Unknown";
            if (_description.Length < 3) 
                _description = _description.PadRight(3, '#');

            if (_description.Length > 15)
            {
                _description = _description.Substring(0, 15).TrimEnd();
                if (_description.Length < 3)
                    _description = _description.PadRight(3, '#');
            }
            if (char.IsLower(_description[0]))
                _description = char.ToUpper(_description[0]) + _description.Substring(1);
        }
    }

    public uint Size { get; set; } = 3;
    public virtual string Info => $"{Description} <{Size}>";
    public override string ToString()
    {
        return $"{GetType().Name.ToUpper()}: {Info}";
    }
}
