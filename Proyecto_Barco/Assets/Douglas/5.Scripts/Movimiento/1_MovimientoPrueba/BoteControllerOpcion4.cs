using UnityEngine;
using UnityEngine.UI;

public class BoteControllerOpcion4 : MonoBehaviour
{
    private Rigidbody boteRb;

    public Slider velocidadSlider; // referencia al Slider desde el Inspector

    public float[] velocidades = { 0f, 3f, 6f, 9f };
    public float velocidadGiro = 50f;

    public Transform flecha; // referencia a la flecha

    void Awake()
    {
        boteRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Obtener valor desde el slider
        int velocidadActual = Mathf.RoundToInt(velocidadSlider.value);
        boteRb.linearVelocity = transform.forward * velocidades[velocidadActual];

        ManejarGiro();
        ActualizarFlecha();
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

    void ActualizarFlecha()
    {
        if (flecha != null)
        {
            // Tomamos el ángulo Y de la rotación del barco
            float anguloY = boteRb.rotation.eulerAngles.y;

            // Como la flecha rota en Z, trasladamos ese ángulo Y al eje Z de la flecha (invirtiéndolo para que apunte bien en pantalla)
            flecha.rotation = Quaternion.Euler(0, 0, -anguloY);
        }
    }
}
