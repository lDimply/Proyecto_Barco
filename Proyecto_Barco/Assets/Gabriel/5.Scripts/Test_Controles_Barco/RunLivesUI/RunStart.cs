using UnityEngine;

public class RunStart : MonoBehaviour
{
    void Start()
    {
        int vidasBase = PlayerPrefs.GetInt("vidasBaseMundo", 3);
        int vidasExtra = SaveData.GetVidasExtra();

        int vidasTotales = vidasBase + vidasExtra;

        Debug.Log($"🧠 Run iniciada con {vidasTotales} vidas (Base: {vidasBase}, Extra: {vidasExtra})");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetRun(vidasTotales);
        }
    }

}
