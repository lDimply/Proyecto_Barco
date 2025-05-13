using UnityEngine;
using System;

public class BoatController : MonoBehaviour
{
    // Fuerza m�xima que se aplicar� al barco al soltar
    public float maxImpulseForce = 20f;

    // Tiempo m�ximo de carga (para calcular cu�nta fuerza aplicar)
    public float maxCharge = 2f;

    // Referencia al Rigidbody del barco
    public Rigidbody rb;

    // Indica si el modo de impulso est� activo
    private bool isActive = false;

    // Indica si el jugador est� cargando el impulso
    private bool isCharging = false;

    // Tiempo acumulado de carga
    private float chargeTime = 0f;

    // Tiempo m�ximo permitido sin usar el impulso antes de cancelarlo
    private float impulseTimeout = 3f;

    // Contador del tiempo desde que se activ� el modo impulso
    private float elapsedTime = 0f;

    // Evento que se invoca cuando termina el impulso (para reactivar el control normal)
    public Action OnImpulseComplete;

    // Llama este m�todo para activar el modo impulso
    public void ActivateImpulseMode()
    {
        isActive = true;
        isCharging = false;
        chargeTime = 0f;
        elapsedTime = 0f;

        // Ralentiza el tiempo para dar efecto de c�mara lenta
        Time.timeScale = 0.3f;
    }

    // Llama este m�todo para salir del modo impulso y volver al modo normal
    public void DeactivateImpulseMode()
    {
        isActive = false;

        // Restaura el tiempo a la normalidad
        Time.timeScale = 1f;

        // Llama al evento para que otro script (como ImpulseZone) sepa que el impulso termin�
        OnImpulseComplete?.Invoke();
    }

    void Update()
    {
        // Si no est� activo, no hacemos nada
        if (!isActive) return;

        // Usamos deltaTime sin escalar (para que el tiempo ralentizado no afecte la l�gica)
        elapsedTime += Time.unscaledDeltaTime;

        // Bloque para PC y m�vil (mouse o touch)
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_ANDROID

        // Mientras se mantenga presionado, acumulamos tiempo de carga
        if (Input.GetMouseButton(0))
        {
            isCharging = true;
            chargeTime += Time.unscaledDeltaTime;
        }

        // Al soltar el bot�n o dedo, se calcula y aplica el impulso
        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            Vector3 dir = GetMouseDirection(); // Direcci�n hacia el mouse o toque
            float force = Mathf.Clamp(chargeTime, 0f, maxCharge) / maxCharge * maxImpulseForce;

            // Aplicamos fuerza en la direcci�n calculada
            rb.AddForce(dir * force, ForceMode.Impulse);

            isCharging = false;

            // Salimos del modo impulso
            DeactivateImpulseMode();
        }
#endif

        // Si pas� demasiado tiempo sin usar el impulso, se cancela autom�ticamente
        if (elapsedTime >= impulseTimeout)
        {
            DeactivateImpulseMode();
        }
    }

    // Calcula la direcci�n desde el barco hacia donde el jugador apunt� (mouse/touch)
    Vector3 GetMouseDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Creamos un plano horizontal a la altura del barco
        Plane plane = new Plane(Vector3.up, transform.position);

        // Si el rayo del mouse toca el plano, calculamos esa direcci�n
        if (plane.Raycast(ray, out float distance))
        {
            Vector3 target = ray.GetPoint(distance);
            return (target - transform.position).normalized;
        }

        // Si no lo logra, devolvemos la direcci�n hacia adelante por defecto
        return transform.forward;
    }

    public float GetChargePercent()
    {
        return Mathf.Clamp(chargeTime / maxCharge, 0f, 1f);
    }

}
