using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public static class GuiUtils
{
    private static PixelPerfectCamera pixelCamera = Camera.main.GetComponent<PixelPerfectCamera>();
    private static Mouse mouse = Mouse.current;


    /// <summary>
    /// Returns the pixel-perfect visual position of the mouse.
    /// </summary>
    /// <returns></returns>
    public static Vector2 GetGuiMousePosition()
    {
        Vector2 mousePos = mouse.position.ReadValue() - new Vector2(Screen.width / 2, Screen.height / 2);
        mousePos /= pixelCamera.pixelRatio;
        return mousePos.Round();
    }
}