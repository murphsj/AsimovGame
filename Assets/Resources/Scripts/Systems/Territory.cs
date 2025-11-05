
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
class TerritoryData
{
    public string name;
    public int3 machines;
    public int3 resistance;
}

[Serializable]
class MapData
{
    /// <summary>
    /// Dictionary of territories on the map.
    /// Key is the ID of the territory, an unsigned 8-bit int.
    /// </summary>
    public Dictionary<byte, TerritoryData> territories;
}

public class Territory
{
    private static MapData mapData;

    public string name;
    public List<Territory> neighbors;
    public TerritoryButton button;

    public static void SetMapData(TextAsset jsonFile)
    {
        mapData = JsonUtility.FromJson<MapData>(jsonFile.text);
    }

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

    public static Territory FromId(byte territoryId, List<Territory> neighbors)
    {
        if (mapData == null) throw new InvalidOperationException(
            "Territory FromId: tried to create a Territory from id with no map data loaded"
        );

        if (!mapData.territories.ContainsKey(territoryId)) throw new ArgumentException(
            "Territory FromId: no map data found for territory id " + territoryId
        );

        TerritoryData data = mapData.territories[territoryId];

        return new Territory(data.name);
    }
}