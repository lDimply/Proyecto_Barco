using UnityEngine;

public class BoteController : MonoBehaviour
{
    private Rigidbody boteRb;
    public float fuerzaBase = 5f;
    public float fuerzaRotacion = 0.2f;

    private Vector2 swipeStartPos;      // Para impulso vertical
    private Vector2 lastTouchPos;       // Para rotación horizontal
    private bool isDragging = false;

    void Awake()
    {
        boteRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swipeStartPos = touch.position;
                lastTouchPos = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 currentPos = touch.position;
                float deltaX = currentPos.x - lastTouchPos.x;

                boteRb.AddTorque(Vector3.up * deltaX * fuerzaRotacion);

                lastTouchPos = currentPos;
            }
            else if (touch.phase == TouchPhase.Ended && isDragging)
            {
                Vector2 swipe = touch.position - swipeStartPos;

                if (swipe.y < -50f && Mathf.Abs(swipe.x) < 100f)
                {
                    ApplyImpulse(Mathf.Abs(swipe.y));
                }

                isDragging = false;
            }
        }
    }

    void ApplyImpulse(float swipeStrength)
    {
        Vector3 impulse = transform.forward * fuerzaBase * (swipeStrength / 100f);
        boteRb.AddForce(impulse, ForceMode.Impulse);
    }
}