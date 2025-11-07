using UnityEngine;

/// <summary>
/// A singleton MonoBehavior responsible for handling simulation of the map.
/// </summary>
public class MapManager : MonoBehaviour
{
    public static MapManager instance { get; private set; }

    private MapLoader loader;

    private void EnforceSingleton()
    {
        // Enforce singleton behavior; delete this instance if one already exists
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }

    void Awake()
    {
        EnforceSingleton();
        loader = GetComponent<MapLoader>();
    }

    void Start()
    {
        loader.LoadMap();
    }
}