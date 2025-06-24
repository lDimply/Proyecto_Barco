using UnityEngine;

public class CoinPopupManager : MonoBehaviour
{
    public GameObject coinPopupPrefab;
    public Transform player;
    public Vector3 offset = new Vector3(0, 2f, 0);
    public Canvas mainCanvas;

    private CoinPopup currentPopup; // ← Referencia al popup activo
    private int currentAmount = 0;

    public void ShowPopup(int amountToAdd = 1)
    {
        if (coinPopupPrefab == null || player == null || mainCanvas == null)
            return;

        // Ya existe un popup activo
        if (currentPopup != null)
        {
            currentAmount += amountToAdd;
            currentPopup.SetAmount(currentAmount);

            // Reiniciar tiempo de vida opcional (si lo ocultas luego de unos segundos)
            currentPopup.RestartLifeTime();
        }
        else
        {
            currentAmount = amountToAdd;

            GameObject popupGO = Instantiate(coinPopupPrefab, mainCanvas.transform);
            Vector3 worldPos = player.position + offset;
            popupGO.transform.position = Camera.main.WorldToScreenPoint(worldPos);

            currentPopup = popupGO.GetComponent<CoinPopup>();
            if (currentPopup != null)
            {
                currentPopup.SetAmount(currentAmount);
                currentPopup.SetTarget(player, offset);
                currentPopup.OnPopupDestroyed += ClearCurrentPopup;
            }
        }
    }

    private void ClearCurrentPopup()
    {
        currentPopup = null;
        currentAmount = 0;
    }
}
