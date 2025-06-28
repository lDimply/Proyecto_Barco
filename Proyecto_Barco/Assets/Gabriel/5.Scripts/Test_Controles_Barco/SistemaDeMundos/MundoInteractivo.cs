using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MundoInteractivo : MonoBehaviour
{
    [Header("Configuración del Mundo")]
    public string nombreMundo = "run_mundo1";
    public bool desbloqueado = false;
    public float tiempoCuentaRegresiva = 3f;

    [Header("Visuales del Mundo")]
    public GameObject visualBloqueado;
    public GameObject visualDesbloqueado;

    [Header("UI de Entrada")]
    public GameObject uiTextoContador; // Este es el Panel_Contador_UI
    public TMP_Text textoContador;     // Este es el Texto_Contador (TextMeshPro)

    private Coroutine cuentaRegresivaCoroutine;

    private void Start()
    {
        if (PlayerPrefs.HasKey(nombreMundo))
            desbloqueado = PlayerPrefs.GetInt(nombreMundo) == 1;

        ActualizarVisual();

        if (uiTextoContador != null)
            uiTextoContador.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!desbloqueado || !other.CompareTag("Player")) return;

        Debug.Log($"Jugador entró a la zona de: {nombreMundo}");

        if (cuentaRegresivaCoroutine == null)
            cuentaRegresivaCoroutine = StartCoroutine(IniciarCuentaRegresiva());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log($"Jugador salió de la zona de: {nombreMundo}");

        if (cuentaRegresivaCoroutine != null)
        {
            StopCoroutine(cuentaRegresivaCoroutine);
            cuentaRegresivaCoroutine = null;

            if (uiTextoContador != null)
                uiTextoContador.SetActive(false);
        }
    }

    private IEnumerator IniciarCuentaRegresiva()
    {
        if (uiTextoContador != null) uiTextoContador.SetActive(true);

        float tiempoRestante = tiempoCuentaRegresiva;

        while (tiempoRestante > 0)
        {
            if (textoContador != null)
                textoContador.text = $"Entrando al mundo en {Mathf.CeilToInt(tiempoRestante)}...";

            yield return new WaitForSeconds(1f);
            tiempoRestante -= 1f;
        }

        Debug.Log($"Cargando escena: {nombreMundo}");
        SceneManager.LoadScene(nombreMundo);
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
}
