using UnityEngine;
using System;

public class BoatController : MonoBehaviour
{
    // Fuerza máxima que se aplicará al barco al soltar
    public float maxImpulseForce = 20f;

    // Tiempo máximo de carga (para calcular cuánta fuerza aplicar)
    public float maxCharge = 2f;

    // Referencia al Rigidbody del barco
    public Rigidbody rb;

    // Indica si el modo de impulso está activo
    private bool isActive = false;

    // Indica si el jugador está cargando el impulso
    private bool isCharging = false;

    // Tiempo acumulado de carga
    private float chargeTime = 0f;

    // Tiempo máximo permitido sin usar el impulso antes de cancelarlo
    private float impulseTimeout = 3f;

    // Contador del tiempo desde que se activó el modo impulso
    private float elapsedTime = 0f;

    // Evento que se invoca cuando termina el impulso (para reactivar el control normal)
    public Action OnImpulseComplete;

    // Llama este método para activar el modo impulso
    public void ActivateImpulseMode()
    {
        isActive = true;
        isCharging = false;
        chargeTime = 0f;
        elapsedTime = 0f;

        // Ralentiza el tiempo para dar efecto de cámara lenta
        Time.timeScale = 0.3f;
    }

    // Llama este método para salir del modo impulso y volver al modo normal
    public void DeactivateImpulseMode()
    {
        isActive = false;

        // Restaura el tiempo a la normalidad
        Time.timeScale = 1f;

        // Llama al evento para que otro script (como ImpulseZone) sepa que el impulso terminó
        OnImpulseComplete?.Invoke();
    }

    void Update()
    {
        // Si no está activo, no hacemos nada
        if (!isActive) return;

        // Usamos deltaTime sin escalar (para que el tiempo ralentizado no afecte la lógica)
        elapsedTime += Time.unscaledDeltaTime;

        // Bloque para PC y móvil (mouse o touch)
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_ANDROID

        // Mientras se mantenga presionado, acumulamos tiempo de carga
        if (Input.GetMouseButton(0))
        {
            isCharging = true;
            chargeTime += Time.unscaledDeltaTime;
        }

        // Al soltar el botón o dedo, se calcula y aplica el impulso
        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            Vector3 dir = GetMouseDirection(); // Dirección hacia el mouse o toque
            float force = Mathf.Clamp(chargeTime, 0f, maxCharge) / maxCharge * maxImpulseForce;

            // Aplicamos fuerza en la dirección calculada
            rb.AddForce(dir * force, ForceMode.Impulse);

            isCharging = false;

            // Salimos del modo impulso
            DeactivateImpulseMode();
        }
#endif

        // Si pasó demasiado tiempo sin usar el impulso, se cancela automáticamente
        if (elapsedTime >= impulseTimeout)
        {
            DeactivateImpulseMode();
        }
    }

    // Calcula la dirección desde el barco hacia donde el jugador apuntó (mouse/touch)
    Vector3 GetMouseDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Creamos un plano horizontal a la altura del barco
        Plane plane = new Plane(Vector3.up, transform.position);

        // Si el rayo del mouse toca el plano, calculamos esa dirección
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 target = ray.GetPoint(distance);
            return (target - transform.position).normalized;
        }

        // Si no lo logra, devolvemos la dirección hacia adelante por defecto
        return transform.forward;
    }

    public float GetChargePercent()
    {
        return Mathf.Clamp(chargeTime / maxCharge, 0f, 1f);
    }

}
