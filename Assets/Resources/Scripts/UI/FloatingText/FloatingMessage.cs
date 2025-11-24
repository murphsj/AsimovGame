using TMPro;
using UnityEngine;

public class FloatingMessage : MonoBehaviour
{
    [SerializeField]
    public float Lifetime = 2;

    [SerializeField]
    TextMeshProUGUI message;

    void Start()
    {
        Destroy(gameObject, Lifetime);
    }

    void Update()
    {
        gameObject.transform.Translate(new Vector3(0, Time.deltaTime * 0.2f, 0));
    }

    public void SetMessage(string text, Color color)
    {
        message.text = text;
        message.color = color;
    }
}