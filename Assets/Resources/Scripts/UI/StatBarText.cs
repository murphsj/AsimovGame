using Services;
using TMPro;
using UnityEngine;

public class StatBarText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI statsText;

    private PlayerStats playerStats;

    private void OnStatBarUpdate()
    {
        statsText.text = "Day "
            + playerStats.Day
            + "\tResources: "
            + playerStats.Resources;
    }

    void Start()
    {
        playerStats = ServiceLocator.Get<PlayerStats>();
        playerStats.BarStatsChanged.AddListener(OnStatBarUpdate);
    }
}