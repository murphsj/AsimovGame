using UnityEngine;
using Services;

[RegisterService]
public class MessageSpawner : MonoBehaviour
{
    [SerializeField]
    public FloatingMessage floatingMessage;

    [SerializeField]
    public Canvas spawnCanvas;

    public void MakeMessage(string text, Color color, Vector2 location)
    {
        FloatingMessage msg = Instantiate(floatingMessage, spawnCanvas.transform);
        msg.GetComponent<RectTransform>().anchoredPosition3D = location;
        msg.SetMessage(text, color);
    }
}