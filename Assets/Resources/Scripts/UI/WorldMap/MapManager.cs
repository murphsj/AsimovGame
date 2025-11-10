using UnityEngine;
using Services;
using UnityEngine.Events;

/// <summary>
/// Handles simulation of the map.
/// </summary>
[RegisterService]
public class MapManager : MonoBehaviour
{
    public UnityEvent AdvanceDay;
    public int day { get; private set; } = 1;

    private MapLoader loader;
    

    void HandleOnDayAdvance()
    {
        day++;
        Debug.Log(day);
    }

    void Awake()
    {
        loader = GetComponent<MapLoader>();
        AdvanceDay.AddListener(HandleOnDayAdvance);
    }

    void Start()
    {
        loader.LoadMap();
    }
}