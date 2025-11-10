using UnityEngine;
using Services;

/// <summary>
/// Handles simulation of the map.
/// </summary>
[RegisterService]
public class MapManager : MonoBehaviour
{
    private MapLoader loader;

    void Awake()
    {
        loader = GetComponent<MapLoader>();
    }

    void Start()
    {
        loader.LoadMap();
    }
}