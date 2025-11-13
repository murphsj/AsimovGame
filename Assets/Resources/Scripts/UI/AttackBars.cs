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

    private const float ANIMATION_TIME = 0.2f;

    private IEnumerator TransitionHeight(float start, float end)
    {
        float timePassed = 0;
        float velocity = 0;
        float height = start;

        transform.localScale = new Vector3(1, start, 1);

        while (timePassed < ANIMATION_TIME)
        {
            timePassed += Time.deltaTime;
            height = Mathf.SmoothDamp(height, end, ref velocity, ANIMATION_TIME);
            transform.localScale = new Vector3(1, height, 1);
            yield return null;
        }

        transform.localScale = new Vector3(1, end, 1);
    }

    public IEnumerator Open()
    {
        gameObject.SetActive(true);
        yield return TransitionHeight(0, 1);
    }

    public IEnumerator Close()
    {
        gameObject.SetActive(false);
        yield return TransitionHeight(0, 1);
    }

    public void UpdateBarProgress(Territory t)
    {
        civBar.SetProgress(t.GetInfectedPercent(MachineType.Civ));
        comBar.SetProgress(t.GetInfectedPercent(MachineType.Civ));
        govBar.SetProgress(t.GetInfectedPercent(MachineType.Civ));
    }
}