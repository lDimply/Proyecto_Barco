using UnityEngine;

public class CoinPopupManager : MonoBehaviour
{
    public GameObject coinPopupPrefab;  // Prefab que se instancia al recoger moneda
    public Transform player;            // El jugador (o el bote) que el texto sigue
    public Vector3 offset = new Vector3(0, 2f, 0); // posición relativa donde aparece

    private CoinPopup currentPopup;     // Referencia al popup activo actual
    private int comboAmount = 0;        // Cuántas monedas seguidas se han recogido

    // 👉 Llamar esto desde ItemPickup para mostrar el popup al recoger
    public void ShowPopup(int amount)
    {
        if (coinPopupPrefab == null || player == null) return;

        // Suma al combo actual
        comboAmount += amount;

        // Si no hay popup activo, crear uno nuevo
        if (currentPopup == null)
        {
            Vector3 popupPosition = player.position + offset;
            GameObject popupObj = Instantiate(coinPopupPrefab, popupPosition, Quaternion.identity);
            currentPopup = popupObj.GetComponent<CoinPopup>();

            if (currentPopup != null)
            {
                currentPopup.SetAmount(comboAmount);
                currentPopup.SetTarget(player);
                currentPopup.OnComboExpired = ResetCombo; // ← Nuevo método para reiniciar
            }
        }
        else
        {
            // Si ya hay uno, solo actualiza su número
            currentPopup.SetAmount(comboAmount);
        }
    }

    private void ResetCombo()
    {
        comboAmount = 0;
        currentPopup = null;
    }

    public void ForceResetCombo()
    {
        comboAmount = 0;
    }


}
