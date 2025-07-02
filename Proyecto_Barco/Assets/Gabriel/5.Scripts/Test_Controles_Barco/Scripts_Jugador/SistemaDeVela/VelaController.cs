using UnityEngine;

public class VelaController : MonoBehaviour
{
    [Header("Estado de la vela")]
    public bool velaIzada = false;

    [Header("Referencia al objeto visual de la vela")]
    public GameObject velaVisual;

    [Header("Configuración del Doble Tap")]
    public float tiempoMaxDobleTap = 0.3f; // Tiempo máximo entre dos toques

    private float tiempoUltimoTap = -1f;

    void Start()
    {
        if (velaVisual != null)
            velaVisual.SetActive(velaIzada);
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        // También funciona con doble click en mouse izquierdo (para testeo en PC)
        if (Input.GetMouseButtonDown(0))
        {
            ProcesarTap();
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ProcesarTap();
        }
#endif
    }

    void ProcesarTap()
    {
        float tiempoActual = Time.time;

        if (tiempoActual - tiempoUltimoTap < tiempoMaxDobleTap)
        {
            ToggleVela();
            tiempoUltimoTap = -1f; // Reiniciamos para evitar triples toques
        }
        else
        {
            tiempoUltimoTap = tiempoActual;
        }
    }

    public void ToggleVela()
    {
        velaIzada = !velaIzada;

        if (velaVisual != null)
            velaVisual.SetActive(velaIzada);

        Debug.Log("Vela izada: " + velaIzada);
    }

    public bool EstaIzada()
    {
        return velaIzada;
    }
}
