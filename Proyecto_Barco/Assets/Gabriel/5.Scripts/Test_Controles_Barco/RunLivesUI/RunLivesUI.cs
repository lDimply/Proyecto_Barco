using UnityEngine;
using System.Collections.Generic;

public class RunLivesUI : MonoBehaviour
{
    [Header("Prefab del ícono de vida")]
    public GameObject vidaUIPrefab;

    [Header("Contenedor donde se instanciarán las vidas")]
    public Transform contenedorVidas;

    private List<GameObject> vidasInstanciadas = new List<GameObject>();

    private void OnEnable()
    {
        GameManager.OnRunLivesChanged += ActualizarUI;
    }

    private void OnDisable()
    {
        GameManager.OnRunLivesChanged -= ActualizarUI;
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            ActualizarUI(GameManager.Instance.runLives);
        }
    }

    private void ActualizarUI(int cantidad)
    {
        // Limpiar vidas anteriores
        foreach (var vida in vidasInstanciadas)
        {
            Destroy(vida);
        }
        vidasInstanciadas.Clear();

        // Agregar nuevas vidas
        for (int i = 0; i < cantidad; i++)
        {
            GameObject nuevaVida = Instantiate(vidaUIPrefab);
            nuevaVida.transform.SetParent(contenedorVidas, false);
            nuevaVida.transform.SetSiblingIndex(0); // ← inserta al inicio

            vidasInstanciadas.Add(nuevaVida);
        }
    }
}
