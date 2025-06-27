using UnityEngine;
using UnityEngine.SceneManagement;

public class MundoInteractivo : MonoBehaviour
{
    [Header("Configuración del Mundo")]
    public string nombreMundo = "run_mundo1";
    public bool desbloqueado = false;
    public MundoInteractivo mundoQueSeDesbloqueaLuego;

    [Header("Visuales del Mundo")]
    public GameObject visualBloqueado;
    public GameObject visualDesbloqueado;

    private void Start()
    {
        // Cargar estado guardado (opcional)
        if (PlayerPrefs.HasKey(nombreMundo))
            desbloqueado = PlayerPrefs.GetInt(nombreMundo) == 1;

        ActualizarVisual();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!desbloqueado) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log($"Jugador entró a la zona de activación de: {nombreMundo}");
            CargarMundo();
        }
    }

    public void Desbloquear()
    {
        desbloqueado = true;
        PlayerPrefs.SetInt(nombreMundo, 1);
        ActualizarVisual();
        Debug.Log($"Mundo desbloqueado: {nombreMundo}");
    }

    private void ActualizarVisual()
    {
        if (visualBloqueado != null) visualBloqueado.SetActive(!desbloqueado);
        if (visualDesbloqueado != null) visualDesbloqueado.SetActive(desbloqueado);
    }

    private void CargarMundo()
    {
        Debug.Log($"Cargando escena: {nombreMundo}");
        SceneManager.LoadScene(nombreMundo);
    }
}
