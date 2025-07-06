using UnityEngine;

public class BotePlayer : MonoBehaviour
{
    private float moveSpeed = 10f;
    private Camera mainCamera;
    [SerializeField] private GameInput gameInput;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        if (mainCamera == null) return;



        // Direcciones de la cámara en el plano XZ
        Vector3 camForward = mainCamera.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = mainCamera.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        // Movimiento en función de la cámara
        Vector3 moveDir = (camForward * inputVector.y + camRight * inputVector.x).normalized;

        // Mover al bote
        transform.position += moveDir * Time.deltaTime * moveSpeed;

        // Rotar suavemente hacia la dirección de movimiento
        if (moveDir != Vector3.zero)
        {
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
    }
}
