using UnityEngine;

public class CoinPopupManager : MonoBehaviour
{
    public GameObject coinPopupPrefab;
    public Transform player;
    public Vector3 offset = new Vector3(0, 2f, 0);

    private CoinPopup currentPopup;
    private int comboAmount = 0;

    public void ShowPopup(int amount)
    {
        if (coinPopupPrefab == null || player == null) return;

        comboAmount += amount;

        if (currentPopup == null)
        {
            Vector3 popupPosition = player.position + offset;
            GameObject popupObj = Instantiate(coinPopupPrefab, popupPosition, Quaternion.identity);
            currentPopup = popupObj.GetComponent<CoinPopup>();

            if (currentPopup != null)
            {
                currentPopup.SetAmount(comboAmount);
                currentPopup.SetTarget(player);
            }
        }
        else
        {
            currentPopup.SetAmount(comboAmount);
        }
    }
}
