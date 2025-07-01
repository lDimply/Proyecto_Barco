using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class RunLivesUI : MonoBehaviour
{
    [Header("Prefab del ícono de vida")]
    public GameObject vidaUIPrefab;

    [Header("Contenedor donde se instanciarán las vidas")]
    public Transform contenedorVidas;

    private List<GameObject> vidasInstanciadas = new List<GameObject>();
    private int vidasActuales = -1; // Para forzar una actualización al inicio

    void Start()
    {
        ActualizarUI();
    }

    void Update()
    {
        if (GameManager.Instance == null) return;

        if (vidasActuales != GameManager.Instance.runLives)
        {
            ActualizarUI();
        }
    }

    void ActualizarUI()
    {
        // Limpiar las vidas anteriores
        foreach (var vida in vidasInstanciadas)
        {
            Destroy(vida);
        }
        vidasInstanciadas.Clear();

        // Agregar vidas nuevas
        vidasActuales = GameManager.Instance.runLives;
        for (int i = 0; i < vidasActuales; i++)
        {
            GameObject nuevaVida = Instantiate(vidaUIPrefab, contenedorVidas);
            vidasInstanciadas.Add(nuevaVida);
        }
    }
}
