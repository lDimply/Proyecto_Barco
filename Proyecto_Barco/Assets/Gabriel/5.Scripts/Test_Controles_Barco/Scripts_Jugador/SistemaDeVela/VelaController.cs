using UnityEngine;

public class VelaController : MonoBehaviour
{
    [Header("Estado de la vela")]
    public bool velaIzada = false;

    [Header("Referencia al objeto visual de la vela")]
    public GameObject velaVisual;

    void Start()
    {
        if (velaVisual != null)
            velaVisual.SetActive(velaIzada);
    }

    public void IzarVela()
    {
        velaIzada = true;
        if (velaVisual != null)
            velaVisual.SetActive(true);
        Debug.Log("✅ Vela izada automáticamente.");
    }

    public void ArriarVela()
    {
        velaIzada = false;
        if (velaVisual != null)
            velaVisual.SetActive(false);
        Debug.Log("🛑 Vela arriada automáticamente.");
    }

    public bool EstaIzada()
    {
        return velaIzada;
    }
}
