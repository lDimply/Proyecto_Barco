using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                // El barco
    public Rigidbody targetRb;              // Rigidbody del barco
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Posici�n relativa
    public float followSpeed = 10f;
    public float rotationSpeed = 5f;

    [Header("Zoom din�mico")]
    public float minZoom = 5f;
    public float maxZoom = 12f;
    public float zoomSpeed = 5f;

    private float currentZoom;

    void Start()
    {
        currentZoom = offset.magnitude;
    }

    void LateUpdate()
    {
        if (!target || !targetRb) return;

        // Calcular velocidad del barco
        float speed = targetRb.linearVelocity.magnitude;
        float zoomFactor = Mathf.InverseLerp(0f, 20f, speed); // Ajusta 20 seg�n tu velocidad m�xima

        // Ajustar zoom (offset)
        float desiredZoom = Mathf.Lerp(minZoom, maxZoom, zoomFactor);
        currentZoom = Mathf.Lerp(currentZoom, desiredZoom, Time.deltaTime * zoomSpeed);

        // Nueva posici�n de la c�mara
        Vector3 desiredPosition = target.position - target.forward * currentZoom + Vector3.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followSpeed);

        // Rotar suavemente hacia la misma direcci�n del barco
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}
