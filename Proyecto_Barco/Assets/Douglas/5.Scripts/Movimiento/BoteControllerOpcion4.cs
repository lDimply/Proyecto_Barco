using UnityEngine;
using UnityEngine.UI;

public class BoteControllerOpcion4 : MonoBehaviour
{
    private Rigidbody boteRb;

    public Slider velocidadSlider; // <-- referencia al Slider desde el Inspector

    public float[] velocidades = { 0f, 3f, 6f, 9f };
    public float velocidadGiro = 50f;

    void Awake()
    {
        boteRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Obtener valor directamente desde el slider
        int velocidadActual = Mathf.RoundToInt(velocidadSlider.value);
        boteRb.linearVelocity = transform.forward * velocidades[velocidadActual];

        ManejarGiro();
    }

    void ManejarGiro()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    if (touch.position.x < Screen.width / 2)
                    {
                        boteRb.MoveRotation(boteRb.rotation * Quaternion.Euler(0, -velocidadGiro * Time.deltaTime, 0));
                    }
                    else
                    {
                        boteRb.MoveRotation(boteRb.rotation * Quaternion.Euler(0, velocidadGiro * Time.deltaTime, 0));
                    }
                }
            }
        }
    }
}