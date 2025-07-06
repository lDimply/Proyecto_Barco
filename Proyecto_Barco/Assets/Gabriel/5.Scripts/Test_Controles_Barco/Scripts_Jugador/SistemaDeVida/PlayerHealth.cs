using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida del Jugador")]
    public int maxHealth = 3;
    public int currentHealth;

    [Header("Invulnerabilidad")]
    public float invulnerabilityTime = 1.0f;
    public float flashInterval = 0.1f; // Tiempo entre parpadeos
    private bool isInvulnerable = false;

    [Header("Referencias")]
    public Renderer playerRenderer; // Asignar en el inspector

    [SerializeField] public CinemachineImpulseSource impulseSource;

    [Header("Eventos")]
    public UnityEvent onPlayerDamaged;
    public UnityEvent onPlayerDeath;

   


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isInvulnerable) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        // Generar camera shake directamente desde impulseSource del jugador
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
            Debug.Log("Impulso generado desde el jugador.");
        }

        onPlayerDamaged?.Invoke();

        if (currentHealth <= 0)
        {
            onPlayerDeath?.Invoke();

            GameManager.Instance.PlayerDied();

            if (GameManager.Instance.runLives > 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else
        {
            StartCoroutine(InvulnerabilityCoroutine());
        }
    }


    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        float timer = 0f;

        while (timer < invulnerabilityTime)
        {
            // Alternar visibilidad
            if (playerRenderer != null)
                playerRenderer.enabled = !playerRenderer.enabled;

            yield return new WaitForSeconds(flashInterval);
            timer += flashInterval;
        }

        // Asegurar que quede visible al final
        if (playerRenderer != null)
            playerRenderer.enabled = true;

        isInvulnerable = false;
        
    }

   
}
