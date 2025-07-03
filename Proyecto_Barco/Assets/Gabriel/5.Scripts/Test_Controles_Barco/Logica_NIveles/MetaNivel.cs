using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaNivel : MonoBehaviour
{
    [Header("Nombre de la escena de ramificación")]
    public string escenaRamificacion = "run_mundo1"; // ← cámbialo desde el Inspector si usas otra escena

    [Header("Efectos opcionales")]
    public GameObject efectoFinalNivel; // (opcional) Partículas, animación, etc.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Nivel completado, regresando a escena de ramificación...");

            // Activar efecto visual (si hay uno asignado)
            if (efectoFinalNivel != null)
                Instantiate(efectoFinalNivel, transform.position, Quaternion.identity);

            // Cargar escena de ramificación
            SceneManager.LoadScene(escenaRamificacion);
        }
    }
}
