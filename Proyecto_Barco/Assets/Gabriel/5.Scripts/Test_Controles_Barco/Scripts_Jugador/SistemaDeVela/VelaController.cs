using UnityEngine;

public class VelaController : MonoBehaviour
{
    [Header("Estado de la vela")]
    public bool velaIzada = false; // ¿La vela está levantada?

    [Header("Referencia al objeto visual de la vela")]
    public GameObject velaVisual; // Asignar el modelo 3D de la vela en el Inspector

    [Header("Tecla para izar/arriar")]
    public KeyCode teclaToggle = KeyCode.V; // Puedes cambiar esta tecla si lo deseas

    void Start()
    {
        // Asegurar que el objeto de la vela se muestre correctamente al iniciar
        if (velaVisual != null)
            velaVisual.SetActive(velaIzada);
    }

    void Update()
    {
        if (Input.GetKeyDown(teclaToggle))
        {
            ToggleVela();
        }
    }

    public void ToggleVela()
    {
        velaIzada = !velaIzada;

        // Mostrar u ocultar el objeto de la vela
        if (velaVisual != null)
            velaVisual.SetActive(velaIzada);

        // Debug
        Debug.Log("Vela izada: " + velaIzada);
    }

    public bool EstaIzada()
    {
        return velaIzada;
    }
}
