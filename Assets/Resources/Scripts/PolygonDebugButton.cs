using UnityEngine;
using UnityEngine.EventSystems;

public class PolygonDebugButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private PolygonRenderer graphic;

    void Start()
    {
        graphic = GetComponent<PolygonRenderer>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        graphic.color = new Color32(255, 0, 0, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        graphic.color = new Color32(255, 255, 255, 255);
    }
}
