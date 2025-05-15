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

        // Este viento se puede eliminar si ahora usas OnTriggerStay
        /*
        if (windZone != null && windZone.IsInWindZone(transform.position))
        {
            Vector3 windForce = windZone.GetWindForce();
            rb.AddForce(windForce, ForceMode.Force);
        }
        */

        if (Mathf.Abs(turnInput) > 0.1f)
        {
            float rotationAmount = turnSpeed * turnInput * Time.fixedDeltaTime;
            transform.Rotate(0f, rotationAmount, 0f);
        }
    }

    void OnTriggerStay(Collider other)
    {
        WindZoneController windZone = other.GetComponent<WindZoneController>();
        if (windZone != null)
        {
            Vector3 windDir = windZone.GetWindDirection().normalized;

            float alignment = Vector3.Dot(transform.forward, windDir);
            float forceFactor = Mathf.Clamp01((alignment + 1f) / 2f);

            Vector3 windForce = windDir * windZone.windStrength * forceFactor;
            rb.AddForce(windForce, ForceMode.Force);

            Debug.Log($"Wind Alignment: {alignment:F2} | Factor: {forceFactor:F2} | Force: {windForce}");
        }
    }
}
