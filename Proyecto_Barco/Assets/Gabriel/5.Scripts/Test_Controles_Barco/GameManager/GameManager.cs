using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Vida de la Run")]
    public int runLives = 3;

    [Header("Nombre de la Escena Lobby")]
    public string lobbySceneName = "Escena_Mundos"; // Cambia por el nombre real


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);


    }


    public void PlayerDied()
    {
        runLives--;

        Debug.Log($"Vidas de la run restantes: {runLives}");

        if (runLives <= 0)
        {
            // Se acabaron las vidas de la run → volver al lobby
            Debug.Log("¡Run terminada! Volviendo al lobby...");
            SceneManager.LoadScene(lobbySceneName);
        }
        else
        {
            // No hacemos nada más, la escena se reinicia desde PlayerHealth
        }
    }

    // Método opcional para reiniciar la run manualmente
    public void ResetRun(int newRunLives)
    {
        runLives = newRunLives;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene(lobbySceneName);
        }
    }


}
