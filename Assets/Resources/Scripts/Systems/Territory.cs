
using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Events;
using System.Linq;
using Services;

[Serializable]
class TerritoryData
{
    public string name;
    public int population;
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
    public static List<Territory> AllTerritories { get; private set; } = new List<Territory>();
    private static MapData mapData;
    private static PlayerStats playerStats;

    public string Name { get; private set; }
    public List<Territory> Neighbors;
    public int Population;
    public int[] Infection;
    public int[] BaseResistance;
    public bool ResourceGainTriggered = false;
    public UnityEvent InfectionChanged;
    
    public TerritoryButton button;

    public static void Init(TextAsset jsonFile)
    {
        mapData = JsonConvert.DeserializeObject<MapData>(jsonFile.text);
        playerStats = ServiceLocator.Get<PlayerStats>();
    }

    Territory(TerritoryData data)
    {
        Name = data.name;
        Population = data.population;
        Infection = new int[] { 0, 0, 0 };
        BaseResistance = data.resistance;
        Neighbors = new List<Territory>();

        for (int i = 0; i < BaseResistance.Count(); i++)
        {
            BaseResistance[i] *= -1;
        }

        AllTerritories.Add(this);
    }

    public float GetInfectedPercent(MachineType mType)
    {
        if (mType == MachineType.ALL)
        {
            int totalInfection = 0;
            for (int i = 0; i < 3; i++)
            {
                totalInfection += Infection[i];
            }
            return (float)totalInfection / Population;
        }
        else
        {
            return (float)Infection[(int)mType] / (Population/3);
        }
    }

    public void SetInfectionLevels(int[] changeLevel)
    {
        for (int i = 0; i < 3; i++)
        {
            SetInfectionLevel((MachineType)i, changeLevel[i]);
        }

        button.UpdateVisuals();
    }

    public void SetInfectionLevel(MachineType mType, int changeLevel)
    {
        Infection[(int)mType] = Math.Clamp(changeLevel, 0, Population/3);
    }

    public bool IsOverInfectionThreshold(float level)
    {
        for (int i = 0; i < 3; i++)
        {
            if ((float)Infection[i] / (Population/3) >= level) return true;
        }

        return false;
    }

    public bool CanBePlayerTargeted()
    {
        if (IsOverInfectionThreshold(playerStats.TargetInfectedThreshhold)) return true;

        foreach (Territory neighbor in Neighbors)
        {
            if (neighbor.IsOverInfectionThreshold(playerStats.TargetInfectedThreshhold)) return true;
        }

        return false;
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