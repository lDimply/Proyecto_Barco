using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonNivel : MonoBehaviour
{
    [Header("Nombre exacto de la escena a cargar")]
    public string nombreEscena;

    /// <summary>
    /// Llama esta función desde el botón para cargar la escena.
    /// </summary>
    public void CargarEscena()
    {
        if (!string.IsNullOrEmpty(nombreEscena))
        {
            Debug.Log($"Cargando escena: {nombreEscena}");
            SceneManager.LoadScene(nombreEscena);
        }
        else
        {
            Debug.LogWarning("No se ha asignado un nombre de escena en el botón.");
        }
    }
}

