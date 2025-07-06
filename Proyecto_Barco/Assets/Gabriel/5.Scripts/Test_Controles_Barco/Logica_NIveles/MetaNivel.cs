using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaNivel : MonoBehaviour
{
    [Header("Referencia al Timer del nivel")]
    public LevelTimer levelTimer;

    [Header("Nombre de la escena de ramificación")]
    public string escenaRamificacion = "run_mundo1";

    private bool nivelCompletado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || nivelCompletado) return;

        nivelCompletado = true;
        levelTimer.DetenerCronometro();

        float tiempoFinal = levelTimer.GetTiempoFinal();
        float tiempoObjetivo = levelTimer.GetTiempoObjetivo();

        Debug.Log($"Tiempo final: {tiempoFinal:F2} / Objetivo: {tiempoObjetivo:F2}");

        if (tiempoFinal <= tiempoObjetivo)
        {
            Debug.Log("✅ Nivel completado dentro del tiempo → Volviendo a la ramificación.");
            SceneManager.LoadScene(escenaRamificacion);
        }
        else
        {
            Debug.Log("❌ Tiempo superado → Reiniciando nivel.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
