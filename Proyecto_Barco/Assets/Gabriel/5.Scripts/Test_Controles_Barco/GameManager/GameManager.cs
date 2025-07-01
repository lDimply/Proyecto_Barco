using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Vida de la Run")]
    [SerializeField]
    private int _runLives = 3;
    public int runLives
    {
        get => _runLives;
        private set
        {
            _runLives = value;
            OnRunLivesChanged?.Invoke(_runLives); // ← Evento se dispara aquí
        }
    }

    public static event Action<int> OnRunLivesChanged; // ← Evento global

    [Header("Nombre de la Escena Lobby")]
    public string lobbySceneName = "Escena_Mundos";

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
            Debug.Log("¡Run terminada! Volviendo al lobby...");
            SceneManager.LoadScene(lobbySceneName);
        }
    }

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
