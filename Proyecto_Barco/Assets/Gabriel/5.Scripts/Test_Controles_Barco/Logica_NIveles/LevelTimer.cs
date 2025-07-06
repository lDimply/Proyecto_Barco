using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [Header("Referencia al jugador (para detectar si se mueve)")]
    public Rigidbody jugadorRB;

    [Header("UI del Timer")]
    public TMP_Text timerText;

    [Header("Tiempo objetivo (segundos)")]
    public float tiempoObjetivo = 10f; // Puedes ajustar en el Inspector

    private float tiempoActual = 0f;
    private bool cronometroActivo = false;
    private bool jugadorEmpezoAMover = false;

    void Update()
    {
        // Detectar si el jugador empezó a moverse
        if (!jugadorEmpezoAMover && jugadorRB != null && jugadorRB.linearVelocity.magnitude > 0.2f)
        {
            jugadorEmpezoAMover = true;
            cronometroActivo = true;
        }

        // Contar tiempo si está activo
        if (cronometroActivo)
        {
            tiempoActual += Time.deltaTime;
            ActualizarUI();
        }
    }

    void ActualizarUI()
    {
        if (timerText != null)
        {
            int segundosActual = Mathf.FloorToInt(tiempoActual);
            int segundosObjetivo = Mathf.FloorToInt(tiempoObjetivo);
            timerText.text = $"{segundosActual:00} / {segundosObjetivo:00}";
        }
    }

    public float GetTiempoFinal()
    {
        return tiempoActual;
    }

    public float GetTiempoObjetivo()
    {
        return tiempoObjetivo;
    }

    public void DetenerCronometro()
    {
        cronometroActivo = false;
    }
}
