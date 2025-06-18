using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotContainer;

    public void UpdateUI(List<InventoryItem> items)
    {
        // Eliminar todos los slots anteriores
        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject);
        }

        // Crear un nuevo slot por cada item
        foreach (InventoryItem invItem in items)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);

            // Aquí pegamos la parte que preguntaste
            Image iconImg = slot.transform.Find("Icon")?.GetComponent<Image>();
            TMP_Text qtyTxt = slot.transform.Find("Cantidad")?.GetComponent<TMP_Text>();

            if (iconImg == null)
            {
                Debug.LogWarning("Prefab ItemSlotUI no tiene hijo 'Icon' con Image.");
            }
            else
            {
                iconImg.sprite = invItem.data.icon;
                iconImg.enabled = true;
            }

            if (qtyTxt != null)
            {
                qtyTxt.text = invItem.data.maxStack > 1 ? invItem.quantity.ToString() : "";
            }
        }
    }
}
