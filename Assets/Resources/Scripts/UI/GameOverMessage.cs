using UnityEngine;
using Services;
using TMPro;
using System.Collections;

[RegisterService]
public class GameOverMessage : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI message;

    private PlayerStats playerStats;
    private const float ANIMATION_TIME = 0.3f;

    private static string PercentString(float perc)
    {
        return string.Format("{0:0}%", perc * 100);
    }

    private IEnumerator TransitionHeight(float start, float end)
    {
        float timePassed = 0;
        transform.localScale = new Vector3(1, start, 1);

        while (timePassed < ANIMATION_TIME)
        {
            timePassed += Time.deltaTime;
            float lerpFactor = Mathf.SmoothStep(0f, 1f, timePassed / ANIMATION_TIME);
            transform.localScale = new Vector3(1, Mathf.Lerp(start, end, lerpFactor), 1);
            yield return null;
        }

        transform.localScale = new Vector3(1, end, 1);
    }

    public IEnumerator Open()
    {
        yield return TransitionHeight(0, 1);
    }

    public void ShowGameOver()
    {
        gameObject.SetActive(true);
        message.text = "GAME OVER\nFinal infection: "
        + PercentString(Territory.GetTotalInfected())
        + "\nThanks for playing!";

        StartCoroutine("Open");
    }

    void Start()
    {
        playerStats = ServiceLocator.Get<PlayerStats>();
        gameObject.SetActive(false);
    }
}
