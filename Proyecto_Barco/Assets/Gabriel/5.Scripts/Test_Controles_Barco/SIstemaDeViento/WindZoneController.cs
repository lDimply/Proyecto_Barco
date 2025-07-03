using UnityEngine;

public class WindZoneController : MonoBehaviour
{
    [Header("Parámetros del viento")]
    public Vector3 windDirection = Vector3.forward;
    public float windStrength = 10f;

    [Header("Zona de viento (usa un Collider con 'IsTrigger')")]
    public Collider windArea;

    private void Reset()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VelaController vela = other.GetComponentInChildren<VelaController>();
            if (vela != null)
                vela.IzarVela();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VelaController vela = other.GetComponentInChildren<VelaController>();
            if (vela != null)
                vela.ArriarVela();
        }
    }

    private void OnDrawGizmos()
    {
        if (windArea != null)
        {
            Gizmos.color = new Color(0f, 1f, 1f, 0.2f);
            Gizmos.DrawCube(windArea.bounds.center, windArea.bounds.size);

            Gizmos.color = Color.cyan;
            Vector3 center = windArea.bounds.center;
            Gizmos.DrawLine(center, center + windDirection.normalized * 3f);
            Gizmos.DrawSphere(center + windDirection.normalized * 3f, 0.1f);
        }
    }
}
