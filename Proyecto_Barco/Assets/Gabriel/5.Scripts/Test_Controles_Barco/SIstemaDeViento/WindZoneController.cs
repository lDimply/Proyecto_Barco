using UnityEngine;

public class WindZoneController : MonoBehaviour
{
    [Header("Parámetros del viento")]
    public Vector3 windDirection = Vector3.forward;
    public float windStrength = 10f;

    [Header("Zona de viento (usa un Collider con 'IsTrigger')")]
    public Collider windArea; // Puede ser un BoxCollider o SphereCollider

    void Reset()
    {
        // Autoasignar si el collider está en el mismo objeto
        if (windArea == null)
            windArea = GetComponent<Collider>();
    }

    public bool IsInWindZone(Vector3 targetPosition)
    {
        if (windArea == null) return false;

        return windArea.bounds.Contains(targetPosition);
    }

    public Vector3 GetWindDirection()
    {
        return windDirection;
    }

   
    public Vector3 GetWindForce()
    {
        return windDirection.normalized * windStrength;
    }

    void OnDrawGizmos()
    {
        if (windArea != null)
        {
            // Dibujar el área de viento (por ejemplo, el BoxCollider)
            Gizmos.color = new Color(0f, 1f, 1f, 0.2f);
            Gizmos.DrawCube(windArea.bounds.center, windArea.bounds.size);

            // Dibujar la dirección del viento desde el centro del área
            Gizmos.color = Color.cyan;
            Vector3 center = windArea.bounds.center;
            Gizmos.DrawLine(center, center + windDirection.normalized * 3f);
            Gizmos.DrawSphere(center + windDirection.normalized * 3f, 0.1f); // punta
        }
    }

}
