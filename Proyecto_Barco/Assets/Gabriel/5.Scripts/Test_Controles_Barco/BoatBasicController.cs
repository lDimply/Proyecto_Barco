using UnityEngine;

public class BoatBasicController : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 15f;
    public float turnSpeed = 50f;
    public float waterDrag = 1f;
    public Rigidbody rb;

    public Joystick joystickDigital;
    public WindZoneController windZone;

    [Range(0f, 1f)]
    public float minWindEfficiency = 0.2f; // fuerza mínima cuando se va contra el viento
    [Range(0f, 1f)]
    public float maxWindEfficiency = 1f;   // fuerza máxima cuando se va con el viento

    void Start()
    {
        rb.linearDamping = waterDrag;
        rb.angularDamping = 2f;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        float moveInput = 0f;
        float turnInput = 0f;

        if (joystickDigital != null && joystickDigital.gameObject.activeInHierarchy)
        {
            moveInput = joystickDigital.Vertical;
            turnInput = joystickDigital.Horizontal;
        }
        else
        {
            moveInput = Input.GetAxis("Vertical");
            turnInput = Input.GetAxis("Horizontal");
        }

        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            Vector3 forwardForce = transform.forward * moveInput * acceleration;
            rb.AddForce(forwardForce, ForceMode.Force);
        }

        // Aplicar viento si el jugador está dentro del área
        if (windZone != null && windZone.IsInWindZone(transform.position))
        {
            Vector3 windDir = windZone.GetWindDirection().normalized;
            float alignment = Vector3.Dot(transform.forward, windDir); // -1 (contra viento) a 1 (a favor)
            float t = (alignment + 1f) / 2f; // convertir a rango 0 - 1
            float forceFactor = Mathf.Lerp(minWindEfficiency, maxWindEfficiency, t);

            Vector3 windForce = windDir * windZone.windStrength * forceFactor;
            rb.AddForce(windForce, ForceMode.Force);

            // DEBUG
            Debug.Log($"[WIND] Align: {alignment:F2}, Factor: {forceFactor:F2}, Force: {windForce}");
        }

        if (Mathf.Abs(turnInput) > 0.1f)
        {
            float rotationAmount = turnSpeed * turnInput * Time.fixedDeltaTime;
            transform.Rotate(0f, rotationAmount, 0f);
        }
    }
}
