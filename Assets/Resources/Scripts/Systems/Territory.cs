
using System.Collections.Generic;

public class Territory
{
    public string name;
    public List<Territory> neighbors;
    public TerritoryButton button;

    public Territory(string name)
    {
        this.name = name;
        neighbors = new List<Territory>();
    }

    public Territory(string name, List<Territory> neighbors)
    {
        this.name = name;
        this.neighbors = neighbors;
    }
}