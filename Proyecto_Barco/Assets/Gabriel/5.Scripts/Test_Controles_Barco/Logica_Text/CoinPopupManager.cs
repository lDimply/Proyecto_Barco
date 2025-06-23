using UnityEngine;

public class CoinPopupManager : MonoBehaviour
{
    [Header("Prefab del popup")]
    public GameObject coinPopupPrefab;

    [Header("Referencia al jugador o bote")]
    public Transform player;

    [Header("Offset para la posición del popup")]
    public Vector3 offset = new Vector3(0, 2f, 0);

    [Header("Canvas principal")]
    public Canvas mainCanvas; // ← Asigna el Canvas de la escena aquí

    public void ShowPopup(int amount)
    {
        if (coinPopupPrefab == null || player == null || mainCanvas == null) return;

        // Instancia el popup como hijo del Canvas
        GameObject popup = Instantiate(coinPopupPrefab, mainCanvas.transform);

        // Posición del popup relativa al jugador
        Vector3 worldPos = player.position + offset;

        // Convertir de posición del mundo a posición de pantalla
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // Asignar posición en la UI
        popup.transform.position = screenPos;

        // Configurar datos
        CoinPopup cp = popup.GetComponent<CoinPopup>();
        if (cp != null)
        {
            cp.SetAmount(amount);
            cp.SetTarget(player); // (opcional si el popup sigue al jugador)
        }
    }
}
