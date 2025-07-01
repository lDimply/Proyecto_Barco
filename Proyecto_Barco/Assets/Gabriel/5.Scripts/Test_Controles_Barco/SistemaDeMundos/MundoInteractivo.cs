using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MundoInteractivo : MonoBehaviour
{
    [Header("Configuraci�n del Mundo")]
    public string nombreMundo = "run_mundo1";       // Nombre exacto de la escena a cargar
    public bool desbloqueado = false;               // Si el mundo est� desbloqueado o no
    public float tiempoCuentaRegresiva = 3f;        // Tiempo que el jugador debe quedarse para entrar

    [Header("Vidas base para este mundo")]
    public int vidasInicialesDeLaRun = 3;

    [Header("Visuales del Mundo")]
    public GameObject visualBloqueado;              // Modelo/visual cuando est� bloqueado
    public GameObject visualDesbloqueado;           // Modelo/visual cuando est� desbloqueado

    [Header("UI de Entrada (Mundo Desbloqueado)")]
    public GameObject uiTextoContador;              // Panel con mensaje de cuenta regresiva
    public TMP_Text textoContador;                  // Texto que muestra el tiempo restante

    [Header("UI de Mundo Bloqueado")]
    public GameObject uiTextoBloqueado;             // Panel que se muestra si el mundo est� bloqueado
    public TMP_Text textoBloqueado;                 // Texto dentro del panel de bloqueo

    private Coroutine cuentaRegresivaCoroutine;

    private void Start()
    {
        // Leer desde PlayerPrefs si el mundo ya fue desbloqueado antes
        if (PlayerPrefs.HasKey(nombreMundo))
            desbloqueado = PlayerPrefs.GetInt(nombreMundo) == 1;

        // Activar visual bloqueado o desbloqueado
        ActualizarVisual();

        // Asegurar que las UIs est�n desactivadas al inicio
        if (uiTextoContador != null)
            uiTextoContador.SetActive(false);

        if (uiTextoBloqueado != null)
            uiTextoBloqueado.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return; // Solo reaccionar al jugador

        if (!desbloqueado)
        {
            // Mostrar mensaje de bloqueo si el mundo no est� disponible
            if (uiTextoBloqueado != null && textoBloqueado != null)
            {
                uiTextoBloqueado.SetActive(true);
                textoBloqueado.text = "Este mundo est� bloqueado...";
                Debug.Log($"Mundo bloqueado: {nombreMundo}");
            }
            return;
        }

        // Si el mundo est� desbloqueado, iniciar la cuenta regresiva
        Debug.Log($"Jugador entr� a la zona de: {nombreMundo}");

        if (cuentaRegresivaCoroutine == null)
            cuentaRegresivaCoroutine = StartCoroutine(IniciarCuentaRegresiva());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log($"Jugador sali� de la zona de: {nombreMundo}");

        // Cancelar cuenta regresiva si el jugador se aleja
        if (cuentaRegresivaCoroutine != null)
        {
            StopCoroutine(cuentaRegresivaCoroutine);
            cuentaRegresivaCoroutine = null;

            if (uiTextoContador != null)
                uiTextoContador.SetActive(false);
        }

        // Ocultar mensaje de bloqueo si estaba activo
        if (uiTextoBloqueado != null)
            uiTextoBloqueado.SetActive(false);
    }

    private IEnumerator IniciarCuentaRegresiva()
    {
        if (uiTextoContador != null)
            uiTextoContador.SetActive(true);

        float tiempoRestante = tiempoCuentaRegresiva;

        while (tiempoRestante > 0)
        {
            if (textoContador != null)
                textoContador.text = $"Entrando al mundo en {Mathf.CeilToInt(tiempoRestante)}...";

            yield return new WaitForSeconds(1f);
            tiempoRestante -= 1f;
        }

        // Cuando termina la cuenta regresiva, se cambia de escena
        Debug.Log($"Cargando escena: {nombreMundo}");

        // Guardar vidas base del mundo en PlayerPrefs
        PlayerPrefs.SetInt("vidasBaseMundo", vidasInicialesDeLaRun);

        SceneManager.LoadScene(nombreMundo);
    }

    // M�todo para desbloquear el mundo desde otro script (por ejemplo, al completar una run)
    public void Desbloquear()
    {
        desbloqueado = true;
        PlayerPrefs.SetInt(nombreMundo, 1); // Guardar desbloqueo
        ActualizarVisual();

        Debug.Log($"Mundo desbloqueado: {nombreMundo}");
    }

    // Muestra el visual correcto dependiendo si el mundo est� desbloqueado o no
    private void ActualizarVisual()
    {
        if (visualBloqueado != null)
            visualBloqueado.SetActive(!desbloqueado);

        if (visualDesbloqueado != null)
            visualDesbloqueado.SetActive(desbloqueado);
    }
}
