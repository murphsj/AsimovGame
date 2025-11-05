
using System.Collections.Generic;

class SectorData
{
    public int machines;
    public int resistance;
}

class TerritoryData
{
    public string name;
    public int civMachines;
    public int corpMachines;
    public int govMachines;
}

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