using Services;
using TMPro;
using UnityEngine;

public class StatBarText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI statsText;

    private PlayerStats playerStats;

    private static string PercentString(float perc)
    {
        return string.Format("{0:0}%", perc * 100);
    }

    private void OnStatBarUpdate()
    {
        statsText.text = "Day "
            + playerStats.Day
            + "\tResources: "
            + playerStats.Resources
            + "\tNotice: Lv"
            + playerStats.NoticeLevel
            + " " + playerStats.NoticeProgress
            + "/" + playerStats.ToNextNoticeLevel
            + "\tTotal Infection: " + PercentString(Territory.GetTotalInfected());
    }

    void Start()
    {
        playerStats = ServiceLocator.Get<PlayerStats>();
        playerStats.BarStatsChanged.AddListener(OnStatBarUpdate);
    }
}