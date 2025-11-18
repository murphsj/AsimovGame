using UnityEngine;
using Services;
using System.Collections;
using System;

[RegisterService]
public class AttackBars : MonoBehaviour
{
    [SerializeField]
    ProgressBar civBar;

    [SerializeField]
    ProgressBar comBar;

    [SerializeField]
    ProgressBar govBar;

    private const float ANIMATION_TIME = 0.3f;
    private RectTransform rect;

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

    public void MoveToTerritory(Territory t)
    {
        rect.anchoredPosition3D = t.button.GetComponent<PolygonRenderer>().GetCenter();
    }

    public IEnumerator Open()
    {
        gameObject.SetActive(true);
        yield return TransitionHeight(0, 1);
    }

    public IEnumerator Close()
    {
        yield return TransitionHeight(1, 0);
        gameObject.SetActive(false);
    }

    public void UpdateBarProgress(Territory t)
    {
        civBar.SetProgress(t.GetInfectedPercent(MachineType.Civ));
        comBar.SetProgress(t.GetInfectedPercent(MachineType.Civ));
        govBar.SetProgress(t.GetInfectedPercent(MachineType.Civ));
    }

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
}