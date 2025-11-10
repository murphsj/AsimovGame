using Services;
using UnityEngine;
using UnityEngine.UI;

public class NextDayButton : MonoBehaviour
{
    private MapManager mapManager;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
    }

    void Start()
    {
        mapManager = ServiceLocator.Get<MapManager>();
    }

    void Clicked()
    {
        mapManager.AdvanceDay.Invoke();
    }
}