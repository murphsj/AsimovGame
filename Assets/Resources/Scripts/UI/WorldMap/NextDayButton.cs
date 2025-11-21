using Services;
using UnityEngine;
using UnityEngine.UI;

public class NextDayButton : MonoBehaviour
{
    private TurnManager turnManager;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
    }

    void Start()
    {
        turnManager = ServiceLocator.Get<TurnManager>();
    }

    void Clicked()
    {
        turnManager.AdvanceDay.Invoke();
    }
}