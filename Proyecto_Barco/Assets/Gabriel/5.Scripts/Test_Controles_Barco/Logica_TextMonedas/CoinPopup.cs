using UnityEngine;
using TMPro;

public class CoinPopup : MonoBehaviour
{
    // 👉 Referencia al componente de texto visual
    [Header("Componentes")]
    public TMP_Text text;

    // 👉 Duración total del popup en pantalla
    public float duration = 2f;

    // 👉 Velocidad a la que flota el popup hacia arriba
    public Vector3 floatSpeed = new Vector3(0, 0.5f, 0);

    // 👉 Escala inicial y máxima del popup según combo
    public Vector3 startScale = Vector3.one;
    public Vector3 maxScale = Vector3.one * 1.5f;

    // 👉 Colores para diferentes niveles de combo
    public Color colorBajo = Color.white;
    public Color colorMedio = Color.yellow;
    public Color colorAlto = Color.red;

    public System.Action OnComboExpired; // ← Nuevo callback público

    // 👉 Contadores internos
    private float timer = 0f;             // tiempo de vida del popup
    private float resetCooldown = 2f;     // cuánto tiempo sin recoger antes de reiniciar combo
    private float resetTimer = 0f;        // tiempo desde la última recogida

    private Transform target;             // el objeto (bote) al que sigue el texto
    private int currentAmount = 0;        // número actual mostrado (+1, +2, etc.)
    private CanvasGroup canvasGroup;      // usado para hacer fade-out del popup

    


    void Awake()
    {
        // Asegura que tenga un CanvasGroup para controlar transparencia
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    void Update()
    {
        // 👉 Siempre sigue al jugador (flotando arriba)
        if (target != null)
        {
            transform.position = target.position + new Vector3(0, 2f, 0);
            transform.LookAt(Camera.main.transform); // que mire hacia la cámara
            transform.Rotate(0, 180, 0);             // corrección de orientación
        }

        // 👉 Movimiento flotante
        transform.position += floatSpeed * Time.deltaTime;

        // 👉 Avanzan los temporizadores
        timer += Time.deltaTime;
        resetTimer += Time.deltaTime;

        // 👉 Comienza a desvanecerse después del 60% de la duración
        float fadeStartTime = duration * 0.6f;
        if (timer >= fadeStartTime)
        {
            float fadeProgress = (timer - fadeStartTime) / (duration - fadeStartTime);
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, fadeProgress);
        }

        // 👉 Destruye el popup completamente al terminar el tiempo
        if (timer >= duration)
        {
            Destroy(gameObject);
        }

        // 👉 Si no se recoge nada después de `resetCooldown`, reinicia combo
        if (resetTimer >= resetCooldown)
        {
            currentAmount = 0;
            text.text = "";
            canvasGroup.alpha = 0f; // desaparece visualmente

            OnComboExpired?.Invoke(); // ← Notifica al manager que debe reiniciar combo
        }

    }

    // 👉 Actualiza el número del popup (al recoger moneda)
    public void SetAmount(int amount)
    {
        currentAmount = amount;
        resetTimer = 0f;     // reinicia cooldown del combo
        timer = 0f;          // reinicia duración
        canvasGroup.alpha = 1f;

        // 👉 Muestra el número en pantalla (+1, +2, ...)
        text.text = "+" + amount;

        // 👉 Escala visual del texto según combo
        transform.localScale = Vector3.Lerp(startScale, maxScale, Mathf.Clamp01(amount / 10f));

        // 👉 Color según nivel de combo
        if (amount < 5)
            text.color = colorBajo;
        else if (amount < 10)
            text.color = colorMedio;
        else
            text.color = colorAlto;
    }

    // 👉 Le asigna un objetivo para seguir (como el bote)
    public void SetTarget(Transform t)
    {
        target = t;
    }
}
