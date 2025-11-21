using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreeManager : MonoBehaviour
{
    [SerializeField]
    private Image upgradeTreeWindow;

    [SerializeField]
    private Button buyButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buyButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
