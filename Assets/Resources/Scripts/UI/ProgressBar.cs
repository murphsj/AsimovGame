using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    Image progressImage;

    public void SetProgress(float progress)
    {
        progressImage.transform.localScale = new Vector3(progress, 1, 1);
    }
}