using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FloatingOnScreenStick : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private RectTransform stickTransform;  // Objeto que contiene el OnScreenStick
    [SerializeField] private Canvas canvas;                 // Tu Canvas principal

    private Camera uiCamera;

    private void Awake()
    {
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            uiCamera = canvas.worldCamera;
    }

    private void Update()
    {
        if (Touchscreen.current == null) return;

        var touch = Touchscreen.current.primaryTouch;

        if (touch.press.wasPressedThisFrame)
        {
            Vector2 screenPos = touch.position.ReadValue();
            Vector2 anchoredPos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                screenPos,
                uiCamera,
                out anchoredPos
            );

            stickTransform.anchoredPosition = anchoredPos;
            stickTransform.gameObject.SetActive(true);
        }
        else if (touch.press.wasReleasedThisFrame)
        {
            stickTransform.gameObject.SetActive(false);
        }
    }
}
