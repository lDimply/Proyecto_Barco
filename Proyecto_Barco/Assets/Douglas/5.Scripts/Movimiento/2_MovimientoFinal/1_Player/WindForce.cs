using UnityEngine;

/// <summary>
/// Aplica una fuerza de viento al Rigidbody cada FixedUpdate.
/// La fuerza la establecen las zonas de viento mediante SetWind().
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class WindForce : MonoBehaviour
{
    [Tooltip("Escala global para todas las fuerzas de viento.")]
    [SerializeField] private float windMultiplier = 1f;

    private Rigidbody rb;
    private Vector3 currentWind = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (currentWind.sqrMagnitude > 0.0001f)
        {
            rb.AddForce(currentWind * windMultiplier, ForceMode.Force);
        }
    }

    /// <summary>Recibe la fuerza en Newtons que debe aplicarse cada física‑tick.</summary>
    public void SetWind(Vector3 newWind) => currentWind = newWind;
}
