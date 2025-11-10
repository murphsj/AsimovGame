
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

    public string Name { get; private set; }
    public List<Territory> Neighbors { get; private set; }
    public int[] Machines { get; private set; }
    public int[] Infection { get; private set; }
    public int[] BaseResistance { get; private set; }
    
    public TerritoryButton button;

    public static void SetMapData(TextAsset jsonFile)
    {
        mapData = JsonConvert.DeserializeObject<MapData>(jsonFile.text);
    }

    Territory(TerritoryData data)
    {
        Name = data.name;
        Machines = data.machines;
        Infection = new int[] { 0, 0, 0 };
        BaseResistance = data.resistance;
        Neighbors = new List<Territory>();
    }

    public float getInfectedPercent(MachineType mType)
    {
        if (mType == MachineType.ALL)
        {
            int totalMachines = 0;
            int totalInfection = 0;
            for (int i = 0; i < 3; i++)
            {
                totalMachines += Machines[i];
                totalInfection += Infection[i];
            }

            return totalInfection / totalMachines;
        } else {
            return Infection[(int)mType] / Machines[(int)mType] * 100;
        }
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
        return new Territory(data);
    }
}