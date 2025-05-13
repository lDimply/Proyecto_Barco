using UnityEngine;

public class BoatBasicController : MonoBehaviour
{
    public float acceleration = 10f;      // Aceleración hacia adelante/atrás
    public float maxSpeed = 15f;          // Velocidad máxima
    public float turnSpeed = 50f;         // Velocidad de giro constante
    public float waterDrag = 1f;          // Resistencia del agua
    public Rigidbody rb;

    void Start()
    {
        // Aplicar fricción del agua
        rb.linearDamping = waterDrag;
        rb.angularDamping = 2f;

        // Congelar rotación no deseada (sólo rotar sobre Y)
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");   // W/S
        float turnInput = Input.GetAxis("Horizontal"); // A/D

        // Movimiento hacia adelante si no se excede la velocidad máxima
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            Vector3 forwardForce = transform.forward * moveInput * acceleration;
            rb.AddForce(forwardForce, ForceMode.Force);
        }

        // Giro constante en el eje Y (sin física)
        if (turnInput != 0f)
        {
            float rotationAmount = turnSpeed * turnInput * Time.fixedDeltaTime;
            transform.Rotate(0f, rotationAmount, 0f);
        }
    }
}
