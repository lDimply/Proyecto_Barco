using UnityEngine;
using TMPro;
using System;

public class CoinPopup : MonoBehaviour
{
    public TMP_Text text;
    public float duration = 1.5f;
    private float timer;
    private Transform target;
    private Vector3 offset;

    public Action OnPopupDestroyed;

    public void SetAmount(int amount)
    {
        text.text = $"+{amount}";

        // Puedes cambiar tamaño/color si quieres aquí según el número
        float scale = 1f + (amount - 1) * 0.1f;
        transform.localScale = Vector3.one * Mathf.Clamp(scale, 1f, 2f);
    }

    public void SetTarget(Transform newTarget, Vector3 newOffset)
    {
        target = newTarget;
        offset = newOffset;
    }

    public void RestartLifeTime()
    {
        timer = duration;
    }

    void Start()
    {
        timer = duration;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + offset);
            transform.position = screenPos;
        }

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            OnPopupDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}
