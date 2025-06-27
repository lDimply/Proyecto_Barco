using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Daño que causa este obstáculo")]
    public int damageAmount = 1;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
            Debug.Log("Jugador recibió daño del obstáculo");
        }
    }
}
