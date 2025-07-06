using UnityEngine;

/// <summary>
/// Zona que aplica viento al jugador cuando entra en su BoxCollider.
/// </summary>
[RequireComponent(typeof(BoxCollider))]
public class WindZone : MonoBehaviour
{
    [Header("Parámetros de Viento")]
    [Tooltip("Dirección local del viento (se convertirá a mundo).")]
    [SerializeField] private Vector3 direction = Vector3.forward;
    [Tooltip("Fuerza en Newtons que se aplicará cada FixedUpdate.")]
    [SerializeField] private float strength = 50f;

    private void Reset()
    {
        // Asegura que el BoxCollider sea trigger al añadir el script.
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WindForce windForce))
        {
            Vector3 worldWind = transform.TransformDirection(direction.normalized) * strength;
            windForce.SetWind(worldWind);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out WindForce windForce))
        {
            windForce.SetWind(Vector3.zero);
        }
    }

#if UNITY_EDITOR   // Dibuja una línea que indica la dirección del viento en la escena
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 start = transform.position;
        Vector3 dir = transform.TransformDirection(direction.normalized) * 2f;
        Gizmos.DrawLine(start, start + dir);
        Gizmos.DrawSphere(start + dir, 0.1f); // punta de la flecha
    }
#endif
}

