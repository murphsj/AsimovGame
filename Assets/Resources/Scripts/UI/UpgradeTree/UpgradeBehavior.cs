using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBehavior : Selectable
{
    [SerializeField]
    private Selectable selectable;
    private bool highlighted;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highlighted = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHighlighted() && !highlighted)
        {
            Debug.Log("Upgrade!!");
            highlighted = true;
        }
        else if (!IsHighlighted())
        {
            highlighted = false;
        }
    }
}
