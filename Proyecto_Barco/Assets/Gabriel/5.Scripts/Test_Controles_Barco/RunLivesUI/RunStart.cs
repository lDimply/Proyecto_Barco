using UnityEngine;

public class RunStart : MonoBehaviour
{
    [Header("Vidas iniciales de la Run")]
    public int vidasIniciales = 3;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetRun(vidasIniciales);
        }
    }
}
