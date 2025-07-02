using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    [Header("Daño que causa este obstáculo")]
    public int damageAmount = 1;

    [Header("Tiempo de invulnerabilidad de colisión")]
    public float tiempoInactivo = 1.5f;

    [Header("Parpadeo visual mientras está inactivo")]
    public float intervaloParpadeo = 0.2f;

    [Header("Fuerza del empujón")]
    public float fuerzaEmpuje = 5f;

    private Collider obstacleCollider;
    private Renderer obstacleRenderer;

    private void Awake()
    {
        obstacleCollider = GetComponent<Collider>();
        obstacleRenderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
            Debug.Log("Jugador recibió daño del obstáculo");

            Rigidbody rbJugador = collision.gameObject.GetComponent<Rigidbody>();
            if (rbJugador != null)
            {
                // Calcular dirección desde el obstáculo hacia el jugador
                Vector3 direccionEmpuje = (collision.transform.position - transform.position).normalized;
                direccionEmpuje.y = 0f; // evitar empuje vertical si es necesario

                // Aplicar fuerza
                rbJugador.AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode.Impulse);
                Debug.Log("Jugador fue empujado fuera del obstáculo");
            }

            StartCoroutine(ParpadeoTemporal());
        }
    }

    private IEnumerator ParpadeoTemporal()
    {
        obstacleCollider.enabled = false;

        float timer = 0f;
        bool visible = true;

        while (timer < tiempoInactivo)
        {
            timer += intervaloParpadeo;

            if (obstacleRenderer != null)
            {
                visible = !visible;
                obstacleRenderer.enabled = visible;
            }

            yield return new WaitForSeconds(intervaloParpadeo);
        }

        if (obstacleRenderer != null)
            obstacleRenderer.enabled = true;

        obstacleCollider.enabled = true;
    }
}
