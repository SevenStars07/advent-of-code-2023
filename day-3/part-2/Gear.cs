namespace part_2;

public class Gear
{
    public int X { get; set; }
    public int Y { get; set; }

    public List<int> NeighboringNumbers { get; set; } = new();
}