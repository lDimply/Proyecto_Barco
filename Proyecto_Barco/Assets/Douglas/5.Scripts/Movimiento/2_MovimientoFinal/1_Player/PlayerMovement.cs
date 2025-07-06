using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento � Parametros")]
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float turnSpeed = 50f;
    [SerializeField] private float waterDrag = 1f;

    [Header("Referencias")]
    private Rigidbody rb;
    private GameInput gameInput;

    private float moveInput;
    private float turnInput;

    private void Awake()
    {
        AssignReferences();
    }

    private void Start()
    {
        SetupRigidbody();
    }

    private void FixedUpdate()
    {
        ReadInput();
        ApplyMovement();
        ApplyRotation();
    }

    // =====================
    // FUNCIONES SEPARADAS
    // =====================

    private void AssignReferences()
    {
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!gameInput) gameInput = FindObjectOfType<GameInput>();
    }

    private void SetupRigidbody()
    {
        if (!rb)
        {
            Debug.LogError("BoatMovement: Rigidbody no encontrado.");
            return;
        }

        rb.linearDamping = waterDrag;
        rb.angularDamping = 2f;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void ReadInput()
    {
        if (!gameInput) return;

        Vector2 input = gameInput.GetMovementVectorNormalized();
        moveInput = Mathf.Clamp(input.y, -1f, 1f);
        turnInput = Mathf.Clamp(input.x, -1f, 1f);
    }

    private void ApplyMovement()
    {
        if (!rb || rb.linearVelocity.magnitude >= maxSpeed) return;

        Vector3 force = transform.forward * moveInput * acceleration;
        rb.AddForce(force, ForceMode.Force);
    }

    private void ApplyRotation()
    {
        if (Mathf.Abs(turnInput) < 0.1f) return;

        float rotationAmount = turnSpeed * turnInput * Time.fixedDeltaTime;
        transform.Rotate(0f, rotationAmount, 0f);
    }

    // (opcional) Si necesitas que otros scripts accedan al input
    public Vector2 GetMovementInput() => new Vector2(turnInput, moveInput);
}
