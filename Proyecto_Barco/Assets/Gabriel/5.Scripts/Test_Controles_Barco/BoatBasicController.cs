using UnityEngine;

public class BoatBasicController : MonoBehaviour
{
    public float acceleration = 10f;       // Aceleración hacia adelante/atrás
    public float maxSpeed = 15f;           // Velocidad máxima
    public float turnSpeed = 50f;          // Velocidad de giro constante
    public float waterDrag = 1f;           // Resistencia del agua
    public Rigidbody rb;

    public Joystick joystickDigital;       // Joystick visual (para móviles)

    void Start()
    {
        rb.linearDamping = waterDrag;
        rb.angularDamping = 2f;

        // Congelar rotaciones en X y Z (solo rotar en Y)
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        float moveInput = 0f;
        float turnInput = 0f;

        // Si el joystick existe y está activo, usamos joystick
        if (joystickDigital != null && joystickDigital.gameObject.activeInHierarchy)
        {
            moveInput = joystickDigital.Vertical;
            turnInput = joystickDigital.Horizontal;
        }
        else
        {
            // Si no hay joystick, usamos teclado (PC)
            moveInput = Input.GetAxis("Vertical");
            turnInput = Input.GetAxis("Horizontal");
        }

        // Movimiento hacia adelante con fuerza limitada
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            Vector3 forwardForce = transform.forward * moveInput * acceleration;
            rb.AddForce(forwardForce, ForceMode.Force);
        }

        // Rotación constante
        if (Mathf.Abs(turnInput) > 0.1f)
        {
            float rotationAmount = turnSpeed * turnInput * Time.fixedDeltaTime;
            transform.Rotate(0f, rotationAmount, 0f);
        }
    }
}
