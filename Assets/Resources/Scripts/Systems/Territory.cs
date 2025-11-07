
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
class TerritoryData
{
    public string name;
    public int[] machines;
    public int[] resistance;
}

[Serializable]
class MapData
{
    /// <summary>
    /// List of territories on the map.
    /// </summary>
    public Dictionary<byte, TerritoryData> territories;
}

public class Territory
{
    private static MapData mapData;

    public string name { get; private set; }
    public List<Territory> neighbors { get; private set; }
    public TerritoryButton button;

    public static void SetMapData(TextAsset jsonFile)
    {
        mapData = JsonConvert.DeserializeObject<MapData>(jsonFile.text);
    }

    public Territory(string name)
    {
        this.name = name;
        neighbors = new List<Territory>();
    }

    public static Territory FromId(byte territoryId)
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