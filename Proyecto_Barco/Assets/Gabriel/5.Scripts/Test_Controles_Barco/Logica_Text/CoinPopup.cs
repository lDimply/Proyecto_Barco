using UnityEngine;
using TMPro;

public class CoinPopup : MonoBehaviour
{
    public TMP_Text text;
    private Transform target;

    public Color lowColor = Color.white;
    public Color mediumColor = Color.yellow;
    public Color highColor = Color.red;

    public float baseFontSize = 24f;
    public float maxFontSize = 48f;

    public float lifetime = 1.5f;
    private float timer;

    public void SetAmount(int amount)
    {
        text.text = $"+{amount}";

        // Cambiar color según valor
        if (amount < 3)
            text.color = lowColor;
        else if (amount < 6)
            text.color = mediumColor;
        else
            text.color = highColor;

        // Escalar tamaño
        float sizeFactor = Mathf.Clamp01(amount / 10f); // 0 a 1
        text.fontSize = Mathf.Lerp(baseFontSize, maxFontSize, sizeFactor);

        timer = 0f;
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    void Update()
    {
        // Seguir al jugador
        if (target != null)
        {
            transform.position = target.position + new Vector3(0, 2f, 0);
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }

        // Auto destrucción con timer
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
