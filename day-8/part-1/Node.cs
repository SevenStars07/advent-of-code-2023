namespace part_1;

public enum Direction
{
    Left,
    Right
}

public class Node
{
    public string Name { get; init; }
    
    public string Left { get; init; }
    
    public Node LeftNode { get; set; }
    public string Right { get; init; }
    
    public Node RightNode { get; set; }
    
    public Node Move(string direction)
    {
        return direction switch
        {
            "L" => LeftNode,
            "R" => RightNode,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
    
    public override string ToString()
    {
        return $"{Name} = ({Left}, {Right})";
    }
}