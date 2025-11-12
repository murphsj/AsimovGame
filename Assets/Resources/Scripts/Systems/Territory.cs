
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections;

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

    public float GetInfectedPercent(MachineType mType)
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

            return (float)totalInfection / totalMachines;
        }
        else
        {
            return (float)Infection[(int)mType] / Machines[(int)mType];
        }
    }

    public void ChangeInfectionLevels(int[] changeLevel)
    {
        for (int i = 0; i < 3; i++)
        {
            Infection[i] += changeLevel[i];
        }

        button.UpdateVisuals();
    }
    
    public IEnumerator ChangeInfectionLevelsAnimated(int[] changeLevel, float animationTime)
    {
        float timePassed = 0;
        float[] velocity = new float[3];
        float[] current = new float[3] { Infection[0], Infection[1], Infection[2] };

        while (timePassed < animationTime)
        {
            timePassed += Time.deltaTime;
            for (int i = 0; i < 3; i++)
            {
                current[i] = Mathf.SmoothDamp(
                    current[i],
                    Infection[i] + changeLevel[i],
                    ref velocity[i],
                    animationTime
                );

                Infection[i] = Mathf.RoundToInt(current[i]);
            }

            button.UpdateVisuals();

            yield return null;
        }

        ChangeInfectionLevels(changeLevel);
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