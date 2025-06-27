using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

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

        onPlayerDamaged?.Invoke();

        if (currentHealth <= 0)
        {
            onPlayerDeath?.Invoke();
            Debug.Log("Jugador ha muerto");

            // Avisamos al sistema global que el jugador murió
            GameManager.Instance.PlayerDied();

            // Solo reiniciar escena si aún quedan vidas de la run
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
        Debug.Log("Jugador ya no es invulnerable");
    }
}
