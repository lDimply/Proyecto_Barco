using UnityEngine;
using TMPro;

public class CoinPopup : MonoBehaviour
{
    [Header("Componentes")]
    public TMP_Text text;
    public float duration = 2f;
    public Vector3 floatSpeed = new Vector3(0, 0.5f, 0);
    public Vector3 startScale = Vector3.one;
    public Vector3 maxScale = Vector3.one * 1.5f;
    public Color colorBajo = Color.white;
    public Color colorMedio = Color.yellow;
    public Color colorAlto = Color.red;

    private float timer = 0f;
    private float resetCooldown = 2f;
    private float resetTimer = 0f;
    private Transform target;

    private int currentAmount = 0;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + new Vector3(0, 2f, 0);
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }

        transform.position += floatSpeed * Time.deltaTime;

        timer += Time.deltaTime;
        resetTimer += Time.deltaTime;

        // Desvanecer poco a poco
        float fadeStartTime = duration * 0.6f;
        if (timer >= fadeStartTime)
        {
            float fadeProgress = (timer - fadeStartTime) / (duration - fadeStartTime);
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, fadeProgress);
        }

        // Destruir después del tiempo total
        if (timer >= duration)
        {
            Destroy(gameObject);
        }

        // Reinicio si pasa demasiado sin recoger
        if (resetTimer >= resetCooldown)
        {
            currentAmount = 0;
            text.text = "";
            canvasGroup.alpha = 0f;
        }
    }

    public void SetAmount(int amount)
    {
        currentAmount = amount;
        resetTimer = 0f;
        timer = 0f;
        canvasGroup.alpha = 1f;

        text.text = "+" + amount;
        transform.localScale = Vector3.Lerp(startScale, maxScale, Mathf.Clamp01(amount / 10f));

        if (amount < 5)
            text.color = colorBajo;
        else if (amount < 10)
            text.color = colorMedio;
        else
            text.color = colorAlto;
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }
}
