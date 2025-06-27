using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [Header("Da�o que causa este obst�culo")]
    public int damageAmount = 1;

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
            Debug.Log("Jugador recibi� da�o del obst�culo");
        }
    }
}
