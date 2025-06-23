using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    [Header("Referencia al ScriptableObject del ítem")]
    public ItemData itemData;

    [Header("Referencia manual al CoinPopupManager")]
    public CoinPopupManager popupManager;  // ← Asignar desde el Inspector

    private void OnTriggerEnter(Collider other)
    {
        InventorySystem inv = other.GetComponent<InventorySystem>();
        if (inv == null || itemData == null) return;

        // Añadir el ítem al inventario
        inv.AddItem(itemData);

        // Mostrar popup si es moneda
        if (itemData.itemType == ItemType.Moneda && popupManager != null)
        {
            popupManager.ShowPopup(1);
        }

        // Destruir el objeto recogido
        Destroy(gameObject);
    }
}
